namespace COMP306_Group_Project_Group_6.DTOs
{   public class AircraftReadDto
    {
        public string AircraftId { get; set; }
        public string Model { get; set; }
        public string Manufacturer { get; set; }
        public int TotalSeats { get; set; }
        public int BusinessClassSeats { get; set; }
        public int EconomyClassSeats { get; set; }
        public int YearManufactured { get; set; }
    }

    public class AircraftCreateDto
    {
        public string AircraftId { get; set; }
        public string Model { get; set; }
        public string Manufacturer { get; set; }
        public int TotalSeats { get; set; }
        public int BusinessClassSeats { get; set; }
        public int EconomyClassSeats { get; set; }
        public int YearManufactured { get; set; }
    }

    public class AircraftUpdateDto
    {
        public string Model { get; set; }
        public string Manufacturer { get; set; }
        public int TotalSeats { get; set; }
        public int BusinessClassSeats { get; set; }
        public int EconomyClassSeats { get; set; }
        public int YearManufactured { get; set; }
    }
}
