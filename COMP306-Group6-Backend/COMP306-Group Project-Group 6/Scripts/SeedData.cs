using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using COMP306_Group_Project_Group_6.Models;

namespace COMP306_Group_Project_Group_6.Scripts
{
    public class SeedData
    {
        public static async Task SeedDynamoDB(IAmazonDynamoDB dynamoDbClient)
        {
            var context = new DynamoDBContext(dynamoDbClient);

            // Seed Airports
            var airports = new List<Airport>
            {
                new Airport { AirportCode = "YYZ", AirportName = "Toronto Pearson International", City = "Toronto", Country = "Canada", Timezone = "EST", NumberOfTerminals = 2, CreatedAt = DateTime.UtcNow },
                new Airport { AirportCode = "YVR", AirportName = "Vancouver International", City = "Vancouver", Country = "Canada", Timezone = "PST", NumberOfTerminals = 3, CreatedAt = DateTime.UtcNow },
                new Airport { AirportCode = "YUL", AirportName = "Montreal-Pierre Elliott Trudeau", City = "Montreal", Country = "Canada", Timezone = "EST", NumberOfTerminals = 1, CreatedAt = DateTime.UtcNow },
                new Airport { AirportCode = "JFK", AirportName = "John F. Kennedy International", City = "New York", Country = "USA", Timezone = "EST", NumberOfTerminals = 6, CreatedAt = DateTime.UtcNow },
                new Airport { AirportCode = "LAX", AirportName = "Los Angeles International", City = "Los Angeles", Country = "USA", Timezone = "PST", NumberOfTerminals = 9, CreatedAt = DateTime.UtcNow },
                new Airport { AirportCode = "LHR", AirportName = "London Heathrow", City = "London", Country = "UK", Timezone = "GMT", NumberOfTerminals = 5, CreatedAt = DateTime.UtcNow },
                new Airport { AirportCode = "CDG", AirportName = "Charles de Gaulle", City = "Paris", Country = "France", Timezone = "CET", NumberOfTerminals = 3, CreatedAt = DateTime.UtcNow },
                new Airport { AirportCode = "NRT", AirportName = "Narita International", City = "Tokyo", Country = "Japan", Timezone = "JST", NumberOfTerminals = 3, CreatedAt = DateTime.UtcNow },
                new Airport { AirportCode = "SYD", AirportName = "Sydney Kingsford Smith", City = "Sydney", Country = "Australia", Timezone = "AEDT", NumberOfTerminals = 3, CreatedAt = DateTime.UtcNow },
                new Airport { AirportCode = "DXB", AirportName = "Dubai International", City = "Dubai", Country = "UAE", Timezone = "GST", NumberOfTerminals = 3, CreatedAt = DateTime.UtcNow }
            };

            foreach (var airport in airports)
            {
                await context.SaveAsync(airport);
            }

            // Seed Aircrafts
            var aircrafts = new List<Aircraft>
            {
                new Aircraft { AircraftId = "AC001", Model = "Boeing 777-300ER", Manufacturer = "Boeing", TotalSeats = 450, BusinessClassSeats = 50, EconomyClassSeats = 400, YearManufactured = 2015, CreatedAt = DateTime.UtcNow },
                new Aircraft { AircraftId = "AC002", Model = "Airbus A320", Manufacturer = "Airbus", TotalSeats = 180, BusinessClassSeats = 20, EconomyClassSeats = 160, YearManufactured = 2018, CreatedAt = DateTime.UtcNow },
                new Aircraft { AircraftId = "AC003", Model = "Boeing 787-9", Manufacturer = "Boeing", TotalSeats = 296, BusinessClassSeats = 30, EconomyClassSeats = 266, YearManufactured = 2019, CreatedAt = DateTime.UtcNow },
                new Aircraft { AircraftId = "AC004", Model = "Airbus A330-300", Manufacturer = "Airbus", TotalSeats = 297, BusinessClassSeats = 27, EconomyClassSeats = 270, YearManufactured = 2016, CreatedAt = DateTime.UtcNow },
                new Aircraft { AircraftId = "AC005", Model = "Boeing 737 MAX", Manufacturer = "Boeing", TotalSeats = 210, BusinessClassSeats = 16, EconomyClassSeats = 194, YearManufactured = 2020, CreatedAt = DateTime.UtcNow },
                new Aircraft { AircraftId = "AC006", Model = "Airbus A350-900", Manufacturer = "Airbus", TotalSeats = 325, BusinessClassSeats = 40, EconomyClassSeats = 285, YearManufactured = 2021, CreatedAt = DateTime.UtcNow },
                new Aircraft { AircraftId = "AC007", Model = "Boeing 777-200LR", Manufacturer = "Boeing", TotalSeats = 317, BusinessClassSeats = 42, EconomyClassSeats = 275, YearManufactured = 2014, CreatedAt = DateTime.UtcNow },
                new Aircraft { AircraftId = "AC008", Model = "Embraer E190", Manufacturer = "Embraer", TotalSeats = 100, BusinessClassSeats = 10, EconomyClassSeats = 90, YearManufactured = 2017, CreatedAt = DateTime.UtcNow },
                new Aircraft { AircraftId = "AC009", Model = "Airbus A220-300", Manufacturer = "Airbus", TotalSeats = 160, BusinessClassSeats = 12, EconomyClassSeats = 148, YearManufactured = 2022, CreatedAt = DateTime.UtcNow },
                new Aircraft { AircraftId = "AC010", Model = "Boeing 767-300ER", Manufacturer = "Boeing", TotalSeats = 269, BusinessClassSeats = 24, EconomyClassSeats = 245, YearManufactured = 2013, CreatedAt = DateTime.UtcNow }
            };

            foreach (var aircraft in aircrafts)
            {
                await context.SaveAsync(aircraft);
            }

            // Seed Flights
            var flights = new List<Flight>
            {
                new Flight { FlightId = "AC100-20250120", FlightNumber = "AC100", DepartureAirport = "YYZ", ArrivalAirport = "YVR", AircraftId = "AC001", DepartureTime = new DateTime(2025, 1, 20, 8, 0, 0), ArrivalTime = new DateTime(2025, 1, 20, 11, 30, 0), BasePrice = 350.00m, Status = "Scheduled", AvailableSeats = 400, CreatedAt = DateTime.UtcNow },
                new Flight { FlightId = "AC101-20250120", FlightNumber = "AC101", DepartureAirport = "YYZ", ArrivalAirport = "JFK", AircraftId = "AC002", DepartureTime = new DateTime(2025, 1, 20, 10, 0, 0), ArrivalTime = new DateTime(2025, 1, 20, 11, 45, 0), BasePrice = 250.00m, Status = "Scheduled", AvailableSeats = 160, CreatedAt = DateTime.UtcNow },
                new Flight { FlightId = "AC200-20250121", FlightNumber = "AC200", DepartureAirport = "YVR", ArrivalAirport = "LAX", AircraftId = "AC003", DepartureTime = new DateTime(2025, 1, 21, 14, 0, 0), ArrivalTime = new DateTime(2025, 1, 21, 17, 0, 0), BasePrice = 300.00m, Status = "Scheduled", AvailableSeats = 266, CreatedAt = DateTime.UtcNow },
                new Flight { FlightId = "AC300-20250122", FlightNumber = "AC300", DepartureAirport = "YUL", ArrivalAirport = "LHR", AircraftId = "AC004", DepartureTime = new DateTime(2025, 1, 22, 18, 0, 0), ArrivalTime = new DateTime(2025, 1, 23, 6, 0, 0), BasePrice = 650.00m, Status = "Scheduled", AvailableSeats = 270, CreatedAt = DateTime.UtcNow },
                new Flight { FlightId = "AC400-20250123", FlightNumber = "AC400", DepartureAirport = "JFK", ArrivalAirport = "CDG", AircraftId = "AC005", DepartureTime = new DateTime(2025, 1, 23, 20, 0, 0), ArrivalTime = new DateTime(2025, 1, 24, 9, 30, 0), BasePrice = 700.00m, Status = "Scheduled", AvailableSeats = 194, CreatedAt = DateTime.UtcNow },
                new Flight { FlightId = "AC500-20250124", FlightNumber = "AC500", DepartureAirport = "LAX", ArrivalAirport = "NRT", AircraftId = "AC006", DepartureTime = new DateTime(2025, 1, 24, 12, 0, 0), ArrivalTime = new DateTime(2025, 1, 25, 16, 0, 0), BasePrice = 850.00m, Status = "Scheduled", AvailableSeats = 285, CreatedAt = DateTime.UtcNow },
                new Flight { FlightId = "AC600-20250125", FlightNumber = "AC600", DepartureAirport = "LHR", ArrivalAirport = "SYD", AircraftId = "AC007", DepartureTime = new DateTime(2025, 1, 25, 21, 0, 0), ArrivalTime = new DateTime(2025, 1, 27, 5, 30, 0), BasePrice = 1200.00m, Status = "Scheduled", AvailableSeats = 275, CreatedAt = DateTime.UtcNow },
                new Flight { FlightId = "AC700-20250126", FlightNumber = "AC700", DepartureAirport = "CDG", ArrivalAirport = "DXB", AircraftId = "AC008", DepartureTime = new DateTime(2025, 1, 26, 15, 0, 0), ArrivalTime = new DateTime(2025, 1, 26, 23, 30, 0), BasePrice = 550.00m, Status = "Scheduled", AvailableSeats = 90, CreatedAt = DateTime.UtcNow },
                new Flight { FlightId = "AC800-20250127", FlightNumber = "AC800", DepartureAirport = "NRT", ArrivalAirport = "YVR", AircraftId = "AC009", DepartureTime = new DateTime(2025, 1, 27, 10, 0, 0), ArrivalTime = new DateTime(2025, 1, 27, 3, 0, 0), BasePrice = 750.00m, Status = "Scheduled", AvailableSeats = 148, CreatedAt = DateTime.UtcNow },
                new Flight { FlightId = "AC900-20250128", FlightNumber = "AC900", DepartureAirport = "SYD", ArrivalAirport = "YYZ", AircraftId = "AC010", DepartureTime = new DateTime(2025, 1, 28, 22, 0, 0), ArrivalTime = new DateTime(2025, 1, 29, 6, 0, 0), BasePrice = 1100.00m, Status = "Scheduled", AvailableSeats = 245, CreatedAt = DateTime.UtcNow }
            };

            foreach (var flight in flights)
            {
                await context.SaveAsync(flight);
            }
        }
    }
}