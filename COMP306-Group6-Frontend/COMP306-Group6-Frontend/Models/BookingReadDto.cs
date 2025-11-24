namespace COMP306_Group6_Frontend.Models
{
    public class BookingReadDto
    {
        public string Id { get; set; } = string.Empty;
        public string FlightId { get; set; } = string.Empty;
        public string PassengerName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public int SeatCount { get; set; }
        public decimal TotalPrice { get; set; }
        public string Status { get; set; } = string.Empty;
    }
}
