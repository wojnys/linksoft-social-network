using api.Dtos.Databaset;
using api.Models;

namespace api.Dtos.User
{
    public class UserDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int FrientId { get; set; }
        public int? DatasetId { get; set; } // Navigation property
        public DatasetDto? Dataset { get; set; } // Navigation property
        // public Dataset? Dataset { get; set; } // Navigation property 

    }
}