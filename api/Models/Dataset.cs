using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class Dataset
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public ICollection<User> Users { get; set; } = new List<User>();

        [NotMapped] // Exclude from DB
        public int UsersCount { get; set; }

        [NotMapped] // Exclude from DB
        public double AverageFriendsPerUser { get; set; }
    }
}