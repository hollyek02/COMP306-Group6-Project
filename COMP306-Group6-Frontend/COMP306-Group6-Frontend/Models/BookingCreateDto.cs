namespace COMP306_Group6_Frontend.Models
{
    public class BookingCreateDto
    {
        public string FlightId { get; set; } = string.Empty;
        public string PassengerName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public int SeatCount { get; set; }
    }
}
