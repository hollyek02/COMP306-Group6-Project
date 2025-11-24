namespace COMP306_Group_Project_Group_6.Models
{
    public class Aircraft
    {
        public string AircraftId { get; set; } // Primary Key (e.g., "AC001")
        public string Model { get; set; }
        public string Manufacturer { get; set; }
        public int TotalSeats { get; set; }
        public int BusinessClassSeats { get; set; }
        public int EconomyClassSeats { get; set; }
        public int YearManufactured { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
