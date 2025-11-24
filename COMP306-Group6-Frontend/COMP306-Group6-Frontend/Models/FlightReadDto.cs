using System.Text.Json.Serialization;

namespace COMP306_Group6_Frontend.Models
{
    public class FlightReadDto
    {
        [JsonPropertyName("flightId")]
        public string FlightId { get; set; } = string.Empty;

        [JsonPropertyName("flightNumber")]
        public string FlightNumber { get; set; } = string.Empty;

        [JsonPropertyName("departureAirport")]
        public string DepartureAirport { get; set; } = string.Empty;

        [JsonPropertyName("arrivalAirport")]
        public string ArrivalAirport { get; set; } = string.Empty;

        [JsonPropertyName("aircraftId")]
        public string AircraftId { get; set; } = string.Empty;

        [JsonPropertyName("departureTime")]
        public DateTime DepartureTime { get; set; }

        [JsonPropertyName("arrivalTime")]
        public DateTime ArrivalTime { get; set; }

        [JsonPropertyName("basePrice")]
        public decimal BasePrice { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; } = string.Empty;

        [JsonPropertyName("availableSeats")]
        public int AvailableSeats { get; set; }
    }
}
