using api.Dtos.Databaset;
using api.Models;

namespace api.Mappers
{
    public static class DatasetMapper
    {
        public static DatasetWithoutUsersDto ToDatasetDtoWithoutUsers(this Dataset datasetModel)
        {

            return new DatasetWithoutUsersDto
            {
                Id = datasetModel.Id,
                Name = datasetModel.Name,
                UsersCount = datasetModel.UsersCount,
                AverageFriendsPerUser = (int)datasetModel.AverageFriendsPerUser
            };
        }
        public static DatasetDto ToDatasetDto(this Dataset datasetModel)
        {
            return new DatasetDto
            {
                Id = datasetModel.Id,
                Name = datasetModel.Name,
                Users = datasetModel.Users.Select(user => user.ToUserDto()).ToList()
            };
        }

        public static Dataset ToDatasetFromCreateRequestDTO(this CreateDatasetRequestDto createDatasetRequestDto)
        {
            return new Dataset
            {
                Name = createDatasetRequestDto.Name
            };
        }
    }
}