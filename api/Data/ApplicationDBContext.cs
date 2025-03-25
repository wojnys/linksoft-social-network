using Microsoft.EntityFrameworkCore;

namespace api.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> dbContentOption) : base(dbContentOption)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // // Configure one-to-many relationship between Dataset and User
            // modelBuilder.Entity<api.Models.User>()
            //     .HasOne(u => u.Dataset) // A User has one Dataset
            //     .WithMany(d => d.Users) // A Dataset has many Users
            //     .HasForeignKey(u => u.DatasetId); // Foreign key in User table
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<api.Models.User> Users { get; set; }
        public DbSet<api.Models.Dataset> Datasets { get; set; }
    }
}