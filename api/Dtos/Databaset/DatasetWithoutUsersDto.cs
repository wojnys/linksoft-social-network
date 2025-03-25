using api.Dtos.User;

namespace api.Dtos.Databaset
{
    public class DatasetWithoutUsersDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int UsersCount { get; set; } = 0;
        public int AverageFriendsPerUser { get; set; } = 0;

    }
}