using api.Data;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDBContext _context;
        public UserRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<List<User>> GetAllAsync()
        {
            return await _context.Users.ToListAsync();
            // Eagerly load the Dataset navigation property
            // return await _context.Users
            //     .Include(u => u.Dataset) // Include the related Dataset
            //     .ToListAsync();

        }

        public async Task<User?> GetByIdAsync(int id)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
            // return await _context.Users
            //             .Include(u => u.Dataset) // Include the related Dataset
            //             .FirstOrDefaultAsync(u => u.Id == id);
        }
    }
}