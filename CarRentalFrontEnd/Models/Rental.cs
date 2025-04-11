using System.ComponentModel.DataAnnotations;

namespace CarRentalFrontEnd.Models
{
    public class Rental
    {

        public int Id { get; set; } // Primary key

        [Required]
        public string? PickupLocation { get; set; }

        [Required]
        public string? DropoffLocation { get; set; }

        [Required]
        public DateTime PickupDate { get; set; }

        [Required]
        public DateTime DropoffDate { get; set; }

        [Required]
        public int RentalDays { get; set; }

        [Required]
        public decimal TotalPayment { get; set; }

        [Required]
        public int CarId { get; set; } // Reference to the car being rented

        public required Car Car { get; set; }
    }
}
