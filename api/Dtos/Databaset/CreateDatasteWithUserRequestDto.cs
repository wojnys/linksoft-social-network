using System.Collections.Generic;

namespace api.Dtos.Databaset
{
    public class CreateDatasetWithUsersRequestDto
    {
        public string DatasetName { get; set; }
        public List<UserRequestDto> Users { get; set; }
    }

    public class UserRequestDto
    {
        public int UserId { get; set; }
        public int FriendId { get; set; }
    }
}