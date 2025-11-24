namespace COMP306_Group_Project_Group_6.DTOs
{
    public class AirportReadDto
    {
        public string AirportCode { get; set; }
        public string AirportName { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Timezone { get; set; }
        public int NumberOfTerminals { get; set; }
    }

    public class AirportCreateDto
    {
        public string AirportCode { get; set; }
        public string AirportName { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Timezone { get; set; }
        public int NumberOfTerminals { get; set; }
    }

    public class AirportUpdateDto
    {
        public string AirportName { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Timezone { get; set; }
        public int NumberOfTerminals { get; set; }
    }
}
