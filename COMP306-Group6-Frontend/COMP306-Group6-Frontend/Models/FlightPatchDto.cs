namespace COMP306_Group6_Frontend.Models
{
    public class FlightPatchDto
    {
        public string? FlightNumber { get; set; }
        public string? DepartureAirport { get; set; }
        public string? ArrivalAirport { get; set; }
        public string? AircraftId { get; set; }
        public DateTime? DepartureTime { get; set; }
        public DateTime? ArrivalTime { get; set; }
        public decimal? BasePrice { get; set; }
        public string? Status { get; set; }
        public int? AvailableSeats { get; set; }
    }
}
