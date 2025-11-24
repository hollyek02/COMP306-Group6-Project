using AutoMapper;
using COMP306_Group_Project_Group_6.DTOs;
using COMP306_Group_Project_Group_6.Models;

namespace COMP306_Group_Project_Group_6.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Airport mappings
            CreateMap<Airport, AirportReadDto>();
            CreateMap<AirportCreateDto, Airport>();
            CreateMap<AirportUpdateDto, Airport>();

            // Aircraft mappings
            CreateMap<Aircraft, AircraftReadDto>();
            CreateMap<AircraftCreateDto, Aircraft>();
            CreateMap<AircraftUpdateDto, Aircraft>();

            // Flight mappings
            CreateMap<Flight, FlightReadDto>();
            CreateMap<FlightCreateDto, Flight>();
            CreateMap<FlightUpdateDto, Flight>();

            // Booking mappings
            CreateMap<Booking, BookingReadDto>();
            CreateMap<BookingCreateDto, Booking>();
            CreateMap<BookingUpdateDto, Booking>();

        }
    }
}