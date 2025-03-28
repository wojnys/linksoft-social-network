using api.Dtos.Databaset;
using api.Dtos.User;
using api.Models;

namespace api.Mappers
{
    public static class UserMapper
    {
        public static UserDto ToUserDto(this User userModel)
        {
            return new UserDto
            {
                Id = userModel.Id,
                UserId = userModel.UserId,
                FrientId = userModel.FrientId,
                DatasetId = userModel.DatasetId,
            };
        }

        public static User ToUserModel(this CreateUserDto request)
        {
            return new User
            {
                UserId = request.UserId,
                FrientId = request.FrientId,
                DatasetId = request.DatasetId,
            };
        }
    }
}