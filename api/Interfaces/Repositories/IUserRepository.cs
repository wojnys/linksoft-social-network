using api.Models;

namespace api.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {

        // Task<List<User>> GetAllAsync();
        // Task<User?> GetByIdAsync(int id);
    }
}