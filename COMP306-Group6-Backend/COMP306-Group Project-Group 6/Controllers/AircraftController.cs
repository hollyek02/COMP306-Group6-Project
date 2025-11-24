using AutoMapper;
using COMP306_Group_Project_Group_6.DTOs;
using COMP306_Group_Project_Group_6.Models;
using COMP306_Group_Project_Group_6.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace COMP306_Group_Project_Group_6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AircraftsController : ControllerBase
    {
        private readonly IRepository<Aircraft> _repository;
        private readonly IMapper _mapper;

        public AircraftsController(IRepository<Aircraft> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        // GET: api/aircrafts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AircraftReadDto>>> GetAll()
        {
            var aircrafts = await _repository.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<AircraftReadDto>>(aircrafts));
        }

        // GET: api/aircrafts/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<AircraftReadDto>> GetById(string id)
        {
            var aircraft = await _repository.GetByIdAsync(id);
            if (aircraft == null)
                return NotFound($"Aircraft with ID {id} not found.");

            return Ok(_mapper.Map<AircraftReadDto>(aircraft));
        }

        // POST: api/aircrafts
        [HttpPost]
        public async Task<ActionResult<AircraftReadDto>> Create([FromBody] AircraftCreateDto aircraftDto)
        {
            if (await _repository.ExistsAsync(aircraftDto.AircraftId))
                return Conflict($"Aircraft with ID {aircraftDto.AircraftId} already exists.");

            var aircraft = _mapper.Map<Aircraft>(aircraftDto);
            aircraft.CreatedAt = DateTime.UtcNow;

            var created = await _repository.CreateAsync(aircraft);
            var readDto = _mapper.Map<AircraftReadDto>(created);

            return CreatedAtAction(nameof(GetById), new { id = readDto.AircraftId }, readDto);
        }

        // PUT: api/aircrafts/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<AircraftReadDto>> Update(string id, [FromBody] AircraftUpdateDto aircraftDto)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null)
                return NotFound($"Aircraft with ID {id} not found.");

            _mapper.Map(aircraftDto, existing);
            existing.UpdatedAt = DateTime.UtcNow;

            var updated = await _repository.UpdateAsync(id, existing);
            return Ok(_mapper.Map<AircraftReadDto>(updated));
        }

        // PATCH: api/aircrafts/{id}
        [HttpPatch("{id}")]
        public async Task<ActionResult<AircraftReadDto>> PartialUpdate(string id, [FromBody] AircraftPatchDto patchDto)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null)
                return NotFound($"Aircraft with ID {id} not found.");

            // Update only non-null properties
            if (patchDto.Model != null) existing.Model = patchDto.Model;
            if (patchDto.Manufacturer != null) existing.Manufacturer = patchDto.Manufacturer;
            if (patchDto.TotalSeats.HasValue) existing.TotalSeats = patchDto.TotalSeats.Value;
            if (patchDto.BusinessClassSeats.HasValue) existing.BusinessClassSeats = patchDto.BusinessClassSeats.Value;
            if (patchDto.EconomyClassSeats.HasValue) existing.EconomyClassSeats = patchDto.EconomyClassSeats.Value;
            if (patchDto.YearManufactured.HasValue) existing.YearManufactured = patchDto.YearManufactured.Value;

            existing.UpdatedAt = DateTime.UtcNow;

            var updated = await _repository.UpdateAsync(id, existing);
            return Ok(_mapper.Map<AircraftReadDto>(updated));
        }

        // DELETE: api/aircrafts/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            var result = await _repository.DeleteAsync(id);
            if (!result)
                return NotFound($"Aircraft with ID {id} not found.");

            return NoContent();
        }
    }
}