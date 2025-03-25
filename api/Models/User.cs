using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace api.Models
{
    public class User
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int FrientId { get; set; }
        public int? DatasetId { get; set; } // Navigation property
        public Dataset? Dataset { get; set; } // Navigation property 

    }
}