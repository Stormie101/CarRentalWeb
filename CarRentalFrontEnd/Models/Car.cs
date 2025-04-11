using System.ComponentModel.DataAnnotations;

namespace CarRentalFrontEnd.Models
{
    public class Car
    {
        public int Id { get; set; } // Primary key

        [Required]
        public string? Model { get; set; } 

        [Required]
        public string? Brand { get; set; } 

        [Required]
        public decimal PricePerDay { get; set; } 

        [Required]
        public string? ImagePath { get; set; }

        public int Availability { get; set; }

        public string? Type { get; set; }
    }
}
