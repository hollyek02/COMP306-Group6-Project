namespace COMP306_Group6_Frontend.Models
{
    public class AircraftCreateDto
    {
        public string AircraftId { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public string Manufacturer { get; set; } = string.Empty;
        public int TotalSeats { get; set; }
        public int BusinessClassSeats { get; set; }
        public int EconomyClassSeats { get; set; }
        public int YearManufactured { get; set; }
    }
}
