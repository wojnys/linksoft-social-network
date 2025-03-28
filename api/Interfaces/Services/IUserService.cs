using api.Dtos.Databaset;
using api.Models;

namespace api.Interfaces.Services
{
    public interface IUserService
    {
        Task<List<User>> CreateUsersAsync(List<UserRequestDto> users, int datasetId);
    }
}