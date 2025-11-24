using Microsoft.AspNetCore.Mvc;
using COMP306_Group6_Frontend.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

public class BookingController : Controller
{
    private readonly BookingService _bookingService;
    private readonly FlightService _flightService;

    public BookingController(BookingService bookingService, FlightService flightService)
    {
        _bookingService = bookingService;
        _flightService = flightService;
    }

    public async Task<IActionResult> Index()
    {
        var bookings = await _bookingService.GetAllAsync();
        return View(bookings);
    }

    public async Task<IActionResult> Details(string id)
    {
        var booking = await _bookingService.GetByIdAsync(id);
        if (booking == null) return NotFound();
        return View(booking);
    }

    public async Task<IActionResult> Create()
    {
        var flights = await _flightService.GetAllAsync();
        ViewBag.Flights = flights;
        return View(new BookingCreateDto());
    }

    [HttpPost]
    public async Task<IActionResult> Create(BookingCreateDto booking)
    {
        if (!ModelState.IsValid)
        {
            var flights = await _flightService.GetAllAsync();
            ViewBag.Flights = flights;
            return View(booking);
        }

        var result = await _bookingService.CreateAsync(booking);
        return RedirectToAction("Details", new { id = result.Id });
    }

    public async Task<IActionResult> Edit(string id)
    {
        var booking = await _bookingService.GetByIdAsync(id);
        if (booking == null) return NotFound();


        var editDto = new BookingCreateDto
        {
            FlightId = booking.FlightId,
            PassengerName = booking.PassengerName,
            Email = booking.Email,
            SeatCount = booking.SeatCount
        };

        return View(editDto);
    }


    [HttpPost]
    public async Task<IActionResult> Edit(string id, BookingCreateDto dto)
    {
        if (!ModelState.IsValid) return View(dto);
        var result = await _bookingService.PutAsync(id, dto);
        return RedirectToAction("Details", new { id });
    }


    public async Task<IActionResult> Patch(string id)
    {
        var booking = await _bookingService.GetByIdAsync(id);
        if (booking == null) return NotFound();
        return View(new BookingPatchDto());
    }


    [HttpPost]
    public async Task<IActionResult> Patch(string id, BookingPatchDto dto)
    {
        var result = await _bookingService.PatchAsync(id, dto);
        return RedirectToAction("Details", new { id });
    }


    public async Task<IActionResult> Delete(string id)
    {
        var booking = await _bookingService.GetByIdAsync(id);
        if (booking == null) return NotFound();
        return View(booking);
    }

    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirmed(string id)
    {
        await _bookingService.DeleteAsync(id);
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> MyBookings(string email)
    {
        var bookings = await _bookingService.GetByEmailAsync(email);
        ViewBag.Email = email;
        return View(bookings);
    }
}
