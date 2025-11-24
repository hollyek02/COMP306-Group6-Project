using AutoMapper;
using COMP306_Group_Project_Group_6.DTOs;
using COMP306_Group_Project_Group_6.Models;
using COMP306_Group_Project_Group_6.Repositories;

using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace COMP306_Group_Project_Group_6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AirportsController : ControllerBase
    {
        private readonly IRepository<Airport> _repository;
        private readonly IMapper _mapper;

        public AirportsController(IRepository<Airport> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        // GET: api/airports
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AirportReadDto>>> GetAll()
        {
            var airports = await _repository.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<AirportReadDto>>(airports));
        }

        // GET: api/airports/{code}
        [HttpGet("{code}")]
        public async Task<ActionResult<AirportReadDto>> GetById(string code)
        {
            var airport = await _repository.GetByIdAsync(code);
            if (airport == null)
                return NotFound($"Airport with code {code} not found.");

            return Ok(_mapper.Map<AirportReadDto>(airport));
        }

        // POST: api/airports
        [HttpPost]
        public async Task<ActionResult<AirportReadDto>> Create([FromBody] AirportCreateDto airportDto)
        {
            if (await _repository.ExistsAsync(airportDto.AirportCode))
                return Conflict($"Airport with code {airportDto.AirportCode} already exists.");

            var airport = _mapper.Map<Airport>(airportDto);
            airport.CreatedAt = DateTime.UtcNow;

            var created = await _repository.CreateAsync(airport);
            var readDto = _mapper.Map<AirportReadDto>(created);

            return CreatedAtAction(nameof(GetById), new { code = readDto.AirportCode }, readDto);
        }

        // PUT: api/airports/{code}
        [HttpPut("{code}")]
        public async Task<ActionResult<AirportReadDto>> Update(string code, [FromBody] AirportUpdateDto airportDto)
        {
            var existing = await _repository.GetByIdAsync(code);
            if (existing == null)
                return NotFound($"Airport with code {code} not found.");

            _mapper.Map(airportDto, existing);
            existing.UpdatedAt = DateTime.UtcNow;

            var updated = await _repository.UpdateAsync(code, existing);
            return Ok(_mapper.Map<AirportReadDto>(updated));
        }

        // PATCH: api/airports/{code}
        [HttpPatch("{code}")]
        public async Task<ActionResult<AirportReadDto>> PartialUpdate(string code, [FromBody] AirportPatchDto patchDto)
        {
            var existing = await _repository.GetByIdAsync(code);
            if (existing == null)
                return NotFound($"Airport with code {code} not found.");

            // Update only non-null properties
            if (patchDto.AirportName != null) existing.AirportName = patchDto.AirportName;
            if (patchDto.City != null) existing.City = patchDto.City;
            if (patchDto.Country != null) existing.Country = patchDto.Country;
            if (patchDto.Timezone != null) existing.Timezone = patchDto.Timezone;
            if (patchDto.NumberOfTerminals.HasValue) existing.NumberOfTerminals = patchDto.NumberOfTerminals.Value;

            existing.UpdatedAt = DateTime.UtcNow;

            var updated = await _repository.UpdateAsync(code, existing);
            return Ok(_mapper.Map<AirportReadDto>(updated));
        }

        // DELETE: api/airports/{code}
        [HttpDelete("{code}")]
        public async Task<ActionResult> Delete(string code)
        {
            var result = await _repository.DeleteAsync(code);
            if (!result)
                return NotFound($"Airport with code {code} not found.");

            return NoContent();
        }
    }
}