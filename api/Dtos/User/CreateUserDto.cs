using api.Dtos.Databaset;
using api.Models;

namespace api.Dtos.User
{
    public class CreateUserDto
    {
        public int UserId { get; set; }
        public int FrientId { get; set; }
        public int DatasetId { get; set; } // Navigation property

    }
}