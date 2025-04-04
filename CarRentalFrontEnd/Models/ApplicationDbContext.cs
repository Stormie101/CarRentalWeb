using Microsoft.EntityFrameworkCore;

namespace CarRentalFrontEnd.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        // DbSet for Users table
        public DbSet<User> Users { get; set; }

    }
}
