using Microsoft.EntityFrameworkCore;

namespace CarRentalFrontEnd.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        // DbSet for Users table
        public DbSet<User> Users { get; set; }

        // DbSet for Feedbacks table
        public DbSet<Feedback> Feedbacks { get; set; }

        //dbset for car
        public DbSet<Car> Cars { get; set; }

        //dbset for rental
        public DbSet<Rental> Rentals { get; set; }

        //dbset for booked
        public DbSet<Booked> Booked { get; set; }

    }
}
