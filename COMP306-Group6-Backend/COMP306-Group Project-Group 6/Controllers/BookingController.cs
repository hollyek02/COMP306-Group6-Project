using AutoMapper;
using COMP306_Group_Project_Group_6.DTOs;
using COMP306_Group_Project_Group_6.Models;
using COMP306_Group_Project_Group_6.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace COMP306_Group_Project_Group_6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly IRepository<Booking> _repository;
        private readonly IRepository<Flight> _flightRepository; // needed for pricing
        private readonly IMapper _mapper;

        public BookingsController(IRepository<Booking> repository, IRepository<Flight> flightRepository, IMapper mapper)
        {
            _repository = repository;
            _flightRepository = flightRepository;
            _mapper = mapper;
        }

        // GET: api/bookings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookingReadDto>>> GetAll()
        {
            var bookings = await _repository.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<BookingReadDto>>(bookings));
        }

        // GET: api/bookings/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<BookingReadDto>> GetById(string id)
        {
            var booking = await _repository.GetByIdAsync(id);
            if (booking == null)
                return NotFound();

            return Ok(_mapper.Map<BookingReadDto>(booking));
        }

        // POST: api/bookings
        [HttpPost]
        public async Task<ActionResult<BookingReadDto>> Create([FromBody] BookingCreateDto dto)
        {
            var booking = _mapper.Map<Booking>(dto);
            var flight = await _flightRepository.GetByIdAsync(dto.FlightId);
            booking.TotalPrice = flight != null ? flight.BasePrice * dto.SeatCount : 0M;
            booking.CreatedAt = DateTime.UtcNow;

            var created = await _repository.CreateAsync(booking);
            var readDto = _mapper.Map<BookingReadDto>(created);

            return CreatedAtAction(nameof(GetById), new { id = readDto.Id }, readDto);
        }

        // PUT: api/bookings/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<BookingReadDto>> Update(string id, [FromBody] BookingUpdateDto dto)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null)
                return NotFound();

            _mapper.Map(dto, existing);
            existing.UpdatedAt = DateTime.UtcNow;

            var updated = await _repository.UpdateAsync(id, existing);
            return Ok(_mapper.Map<BookingReadDto>(updated));
        }

        // PATCH: api/bookings/{id}
        [HttpPatch("{id}")]
        public async Task<ActionResult<BookingReadDto>> Patch(string id, [FromBody] BookingPatchDto patchDto)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null)
                return NotFound();

            if (patchDto.SeatCount.HasValue) existing.SeatCount = patchDto.SeatCount.Value;
            if (patchDto.Status != null) existing.Status = patchDto.Status;

            existing.UpdatedAt = DateTime.UtcNow;

            var updated = await _repository.UpdateAsync(id, existing);
            return Ok(_mapper.Map<BookingReadDto>(updated));
        }


        // DELETE: api/bookings/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            var result = await _repository.DeleteAsync(id);
            if (!result)
                return NotFound();

            return NoContent();
        }

    }
}
