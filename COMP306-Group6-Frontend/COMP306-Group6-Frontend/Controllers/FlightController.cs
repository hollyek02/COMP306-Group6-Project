using Microsoft.AspNetCore.Mvc;
using COMP306_Group6_Frontend.Models;

public class FlightController : Controller
{
    private readonly FlightService _service;
    public FlightController(FlightService service) => _service = service;

    public async Task<IActionResult> Index() =>
        View(await _service.GetAllAsync());

    public async Task<IActionResult> Details(string id)
    {
        var item = await _service.GetByIdAsync(id);
        if (item == null) return NotFound();
        return View(item);
    }

    public IActionResult Create() => View();

    [HttpPost]
    public async Task<IActionResult> Create(FlightCreateDto dto)
    {
        if (!ModelState.IsValid) return View(dto);
        await _service.CreateAsync(dto);
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Edit(string id)
    {
        var item = await _service.GetByIdAsync(id);
        if (item == null) return NotFound();
        var updateDto = new FlightUpdateDto
        {
            FlightNumber = item.FlightNumber,
            DepartureAirport = item.DepartureAirport,
            ArrivalAirport = item.ArrivalAirport,
            AircraftId = item.AircraftId,
            DepartureTime = item.DepartureTime,
            ArrivalTime = item.ArrivalTime,
            BasePrice = item.BasePrice,
            Status = item.Status,
            AvailableSeats = item.AvailableSeats
        };
        ViewBag.FlightId = item.FlightId;
        return View(updateDto);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(string id, FlightUpdateDto dto)
    {
        if (!ModelState.IsValid) return View(dto);
        await _service.PutAsync(id, dto);
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Patch(string id)
    {
        var item = await _service.GetByIdAsync(id);
        if (item == null) return NotFound();
        ViewBag.FlightId = id;
        return View(new FlightPatchDto());
    }

    [HttpPost]
    public async Task<IActionResult> Patch(string id, FlightPatchDto dto)
    {
        if (!ModelState.IsValid) return View(dto);
        await _service.PatchAsync(id, dto);
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Delete(string id)
    {
        await _service.DeleteAsync(id);
        return RedirectToAction("Index");
    }
}
