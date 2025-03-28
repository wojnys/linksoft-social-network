using api.Dtos.Databaset;
using api.Interfaces.Services;
using api.Models;

namespace api.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<List<User>> CreateUsersAsync(List<UserRequestDto> users, int datasetId)
        {

            var userModels = users.Select(u => new User
            {
                UserId = u.UserId,
                FrientId = u.FriendId,
                DatasetId = datasetId
            }).ToList();

            await _unitOfWork.Users.AddRangeAsync(userModels);
            await _unitOfWork.Complete();

            return userModels;
        }
    }
}