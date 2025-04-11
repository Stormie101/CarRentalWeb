namespace CarRentalFrontEnd.Models
{
    public class Feedback
    {
        public int Id { get; set; } // Primary key
        public string? OverallSatisfaction { get; set; }
        public string? CarCondition { get; set; }
        public string? PickupProcess { get; set; }
        public string? DropoffProcess { get; set; }
        public string? CustomerService { get; set; }
        public string? AdditionalFeedback { get; set; }
        public DateTime SubmittedAt { get; set; } = DateTime.Now; // Automatically sets timestamp
    }
}

