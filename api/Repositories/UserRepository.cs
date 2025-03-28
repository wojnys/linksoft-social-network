using api.Data;
using api.Dtos.Databaset;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly ApplicationDBContext _context;

        public UserRepository(ApplicationDBContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<User>> CreateUsersAsync(List<UserRequestDto> users, int datasetId)
        {
            var userModels = users.Select(u => new User
            {
                UserId = u.UserId,
                FrientId = u.FriendId,
                DatasetId = datasetId
            }).ToList();

            await AddRangeAsync(userModels);
            await _context.SaveChangesAsync();

            return userModels;
        }

        // public async Task<List<User>> GetAllAsync()
        // {
        //     return await _context.Users.ToListAsync();
        //     // Eagerly load the Dataset navigation property
        //     // return await _context.Users
        //     //     .Include(u => u.Dataset) // Include the related Dataset
        //     //     .ToListAsync();

        // }

        // public async Task<User?> GetByIdAsync(int id)
        // {
        //     return await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
        //     // return await _context.Users
        //     //             .Include(u => u.Dataset) // Include the related Dataset
        //     //             .FirstOrDefaultAsync(u => u.Id == id);
        // }
    }
}