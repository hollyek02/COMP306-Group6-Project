namespace COMP306_Group_Project_Group_6.Models
{
    public class Booking
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string FlightId { get; set; } = string.Empty;
        public string PassengerName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public int SeatCount { get; set; }
        public decimal TotalPrice { get; set; }
        public string Status { get; set; } = "Active";
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
    }
}
