using api.Dtos.User;

namespace api.Dtos.Databaset
{
    public class DatasetDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public ICollection<UserDto> Users { get; set; } = new List<UserDto>();
    }
}