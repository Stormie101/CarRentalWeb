namespace CarRentalFrontEnd.Models
{
    public class Booked
    {
        public int Id { get; set; } // Primary Key
        public int CarId { get; set; }
        public string? CarName { get; set; }
        public string? CarImage { get; set; }
        public string? PickupLocation { get; set; }
        public string? DropoffLocation { get; set; }
        public DateTime PickupDate { get; set; }
        public DateTime DropoffDate { get; set; }
        public decimal TotalPrice { get; set; }
    }

}
