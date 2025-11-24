namespace COMP306_Group_Project_Group_6.Models
{
    public class Flight
    {
        public string FlightId { get; set; } // Primary Key (e.g., "AC101-20250115")
        public string FlightNumber { get; set; }
        public string DepartureAirport { get; set; }
        public string ArrivalAirport { get; set; }
        public string AircraftId { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }
        public decimal BasePrice { get; set; }
        public string Status { get; set; } // Scheduled, Delayed, Cancelled, Completed
        public int AvailableSeats { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
