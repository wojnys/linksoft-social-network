using api.Data;
using api.Dtos.Databaset;
using api.Interfaces;
using api.Mappers;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class DatasetRepository : GenericRepository<Dataset>, IDatasetRepository
    {
        private readonly ApplicationDBContext _context;

        public DatasetRepository(ApplicationDBContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> IsDatasetNameAvailable(string datasetName)
        {
            var datasetExists = await _context.Datasets.AnyAsync(u => u.Name == datasetName);
            return !datasetExists; // Return true if the name is available, false otherwise
        }

        public async Task<IEnumerable<int>> GetUserIdsForDataset(int datasetId)
        {
            return await _context.Users
                .Where(u => u.DatasetId == datasetId)
                .Select(u => u.UserId)
                .Distinct()
                .ToListAsync();
        }

        public async Task<IEnumerable<int>> GetFriendIdsForDataset(int datasetId)
        {
            return await _context.Users
                .Where(u => u.DatasetId == datasetId)
                .Select(u => u.FrientId)  // Fixed typo
                .Distinct()
                .ToListAsync();
        }

        public async Task<List<(int UserId, int Count)>> GetUserFriendCountsForDataset(int datasetId)
        {
            var userIdCounts = await _context.Users
                .Where(u => u.DatasetId == datasetId)
                .GroupBy(u => u.UserId)
                .Select(g => new { UserId = g.Key, Count = g.Count() })
                .ToListAsync();

            var friendIdCounts = await _context.Users
                .Where(u => u.DatasetId == datasetId)
                .GroupBy(u => u.FrientId)
                .Select(g => new { UserId = g.Key, Count = g.Count() })
                .ToListAsync();

            // Return combined data to the service layer for further processing
            return userIdCounts.Concat(friendIdCounts)
                .GroupBy(x => x.UserId)
                .Select(g => (UserId: g.Key, Count: g.Sum(x => x.Count)))
                .ToList();
        }


        public async Task<IEnumerable<Dataset>> GetAllWithUsersAsync()
        {
            return await _context.Datasets.Include(u => u.Users).ToListAsync();
        }

    }
}