using api.Data;
using api.Dtos.Databaset;
using api.Interfaces;
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

        public async Task<Dataset?> AddDatasetWithUsersAsync(CreateDatasetWithUsersRequestDto request)
        {
            // Start a transaction
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {

                if (await IsDatasetNameAvailable(request.DatasetName) == false)
                {
                    await transaction.RollbackAsync();
                    return null;
                }


                var datasetModel = new Dataset
                {
                    Name = request.DatasetName
                };
                await _context.Datasets.AddAsync(datasetModel);
                await _context.SaveChangesAsync();


                var users = request.Users.Select(u => new User
                {
                    UserId = (int)u.UserId,
                    FrientId = (int)u.FriendId,
                    DatasetId = datasetModel.Id
                }).ToList();

                await _context.Users.AddRangeAsync(users);
                await _context.SaveChangesAsync();

                await transaction.CommitAsync();

                datasetModel.UsersCount = await GetUsersCountForDataset(datasetModel.Id);
                datasetModel.AverageFriendsPerUser = await GetAvarageUserFriendsForDataset(datasetModel.Id);

                return datasetModel;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<bool> IsDatasetNameAvailable(string datasetName)
        {
            var datasetExists = await _context.Datasets.AnyAsync(u => u.Name == datasetName);
            return !datasetExists; // Return true if the name is available, false otherwise
        }
        public async Task<int> GetUsersCountForDataset(int datasetId)
        {
            var userIds = await _context.Users
                .Where(u => u.DatasetId == datasetId)
                .Select(u => u.UserId)
                .Distinct()
                .ToListAsync();

            var friendIds = await _context.Users
                .Where(u => u.DatasetId == datasetId)
                .Select(u => u.FrientId)
                .ToListAsync();

            int uniqueUsers = userIds.Union(friendIds).Distinct().Count();
            return uniqueUsers;
        }

        public async Task<int> GetAvarageUserFriendsForDataset(int datasetId)
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

            // Combine and sum the counts
            var combinedCounts = userIdCounts
                .Concat(friendIdCounts)
                .GroupBy(x => x.UserId)
                .Select(g => new
                {
                    UserId = g.Key,
                    Count = g.Sum(x => x.Count)
                })
                .OrderByDescending(x => x.Count)
                .ToList();

            int sum = combinedCounts.Sum(x => x.Count);
            int count = combinedCounts.Count;


            if (count == 0)
            {
                return 0;
            }
            return sum / count;
        }

        public async Task<IEnumerable<Dataset>> GetAllWithUsersAsync()
        {
            return await _context.Datasets.Include(u => u.Users).ToListAsync();
        }

        public async Task<IEnumerable<Dataset>> GetAllWithUserStatsAsync()
        {
            var datasets = await GetAllAsync();
            foreach (var dataset in datasets)
            {
                dataset.UsersCount = await GetUsersCountForDataset(dataset.Id);
                dataset.AverageFriendsPerUser = await GetAvarageUserFriendsForDataset(dataset.Id);
            }
            return datasets;
        }
    }
}