using api.Dtos.Databaset;
using api.Models;

namespace api.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<List<User>> CreateUsersAsync(List<UserRequestDto> users, int datasetId);
        // Task<List<User>> GetAllAsync();
        // Task<User?> GetByIdAsync(int id);
    }
}