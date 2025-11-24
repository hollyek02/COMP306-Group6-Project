using AutoMapper;
using COMP306_Group_Project_Group_6.DTOs;
using COMP306_Group_Project_Group_6.Models;
using COMP306_Group_Project_Group_6.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace COMP306_Group_Project_Group_6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightsController : ControllerBase
    {
        private readonly IRepository<Flight> _repository;
        private readonly IMapper _mapper;

        public FlightsController(IRepository<Flight> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        // GET: api/flights
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FlightReadDto>>> GetAll()
        {
            var flights = await _repository.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<FlightReadDto>>(flights));
        }

        // GET: api/flights/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<FlightReadDto>> GetById(string id)
        {
            var flight = await _repository.GetByIdAsync(id);
            if (flight == null)
                return NotFound($"Flight with ID {id} not found.");

            return Ok(_mapper.Map<FlightReadDto>(flight));
        }

        // POST: api/flights
        [HttpPost]
        public async Task<ActionResult<FlightReadDto>> Create([FromBody] FlightCreateDto flightDto)
        {
            if (await _repository.ExistsAsync(flightDto.FlightId))
                return Conflict($"Flight with ID {flightDto.FlightId} already exists.");

            var flight = _mapper.Map<Flight>(flightDto);
            flight.CreatedAt = DateTime.UtcNow;

            var created = await _repository.CreateAsync(flight);
            var readDto = _mapper.Map<FlightReadDto>(created);

            return CreatedAtAction(nameof(GetById), new { id = readDto.FlightId }, readDto);
        }

        // PUT: api/flights/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<FlightReadDto>> Update(string id, [FromBody] FlightUpdateDto flightDto)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null)
                return NotFound($"Flight with ID {id} not found.");

            _mapper.Map(flightDto, existing);
            existing.UpdatedAt = DateTime.UtcNow;

            var updated = await _repository.UpdateAsync(id, existing);
            return Ok(_mapper.Map<FlightReadDto>(updated));
        }

        // PATCH: api/flights/{id}
        [HttpPatch("{id}")]
        public async Task<ActionResult<FlightReadDto>> PartialUpdate(string id, [FromBody] FlightPatchDto patchDto)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null)
                return NotFound($"Flight with ID {id} not found.");

            // Update only non-null properties
            if (patchDto.FlightNumber != null) existing.FlightNumber = patchDto.FlightNumber;
            if (patchDto.DepartureAirport != null) existing.DepartureAirport = patchDto.DepartureAirport;
            if (patchDto.ArrivalAirport != null) existing.ArrivalAirport = patchDto.ArrivalAirport;
            if (patchDto.AircraftId != null) existing.AircraftId = patchDto.AircraftId;
            if (patchDto.DepartureTime.HasValue) existing.DepartureTime = patchDto.DepartureTime.Value;
            if (patchDto.ArrivalTime.HasValue) existing.ArrivalTime = patchDto.ArrivalTime.Value;
            if (patchDto.BasePrice.HasValue) existing.BasePrice = patchDto.BasePrice.Value;
            if (patchDto.Status != null) existing.Status = patchDto.Status;
            if (patchDto.AvailableSeats.HasValue) existing.AvailableSeats = patchDto.AvailableSeats.Value;

            existing.UpdatedAt = DateTime.UtcNow;

            var updated = await _repository.UpdateAsync(id, existing);
            return Ok(_mapper.Map<FlightReadDto>(updated));
        }

        // DELETE: api/flights/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            var result = await _repository.DeleteAsync(id);
            if (!result)
                return NotFound($"Flight with ID {id} not found.");

            return NoContent();
        }
    }
}