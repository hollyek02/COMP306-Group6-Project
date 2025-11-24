namespace COMP306_Group6_Frontend.Models
{
    public class AirportUpdateDto
    {
        public string AirportName { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string Timezone { get; set; } = string.Empty;
        public int NumberOfTerminals { get; set; }
    }
}
