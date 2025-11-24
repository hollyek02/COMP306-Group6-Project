using Microsoft.AspNetCore.Mvc;
using COMP306_Group6_Frontend.Models;

public class AircraftController : Controller
{
    private readonly AircraftService _service;
    public AircraftController(AircraftService service) => _service = service;

    public async Task<IActionResult> Index() => View(await _service.GetAllAsync());

    public async Task<IActionResult> Details(string id)
    {
        var item = await _service.GetByIdAsync(id);
        if (item == null) return NotFound();
        return View(item);
    }
    public IActionResult Create() => View();

    [HttpPost]
    public async Task<IActionResult> Create(AircraftCreateDto dto)
    {
        if (!ModelState.IsValid) return View(dto);
        await _service.CreateAsync(dto);
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Edit(string id)
    {
        var item = await _service.GetByIdAsync(id);
        if (item == null) return NotFound();
        var updateDto = new AircraftUpdateDto
        {
            Model = item.Model,
            Manufacturer = item.Manufacturer,
            TotalSeats = item.TotalSeats,
            BusinessClassSeats = item.BusinessClassSeats,
            EconomyClassSeats = item.EconomyClassSeats,
            YearManufactured = item.YearManufactured
        };
        ViewBag.AircraftId = item.AircraftId;
        return View(updateDto);
    }
    [HttpPost]
    public async Task<IActionResult> Edit(string id, AircraftUpdateDto dto)
    {
        if (!ModelState.IsValid) return View(dto);
        await _service.PutAsync(id, dto);
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Patch(string id)
    {
        var item = await _service.GetByIdAsync(id);
        if (item == null) return NotFound();
        ViewBag.AircraftId = id;
        return View(new AircraftPatchDto());
    }
    [HttpPost]
    public async Task<IActionResult> Patch(string id, AircraftPatchDto dto)
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
