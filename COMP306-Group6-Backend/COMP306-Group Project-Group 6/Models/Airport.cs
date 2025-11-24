namespace COMP306_Group_Project_Group_6.Models
{
    public class Airport
    {
        public string AirportCode { get; set; } // Primary Key (e.g., "YYZ")
        public string AirportName { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Timezone { get; set; }
        public int NumberOfTerminals { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }

}
