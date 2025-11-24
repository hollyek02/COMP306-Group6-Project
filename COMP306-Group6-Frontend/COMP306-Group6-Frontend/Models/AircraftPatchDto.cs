namespace COMP306_Group6_Frontend.Models
{
    public class AircraftPatchDto
    {
        public string? Model { get; set; }
        public string? Manufacturer { get; set; }
        public int? TotalSeats { get; set; }
        public int? BusinessClassSeats { get; set; }
        public int? EconomyClassSeats { get; set; }
        public int? YearManufactured { get; set; }
    }
}
