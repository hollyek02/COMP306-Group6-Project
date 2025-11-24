using Microsoft.AspNetCore.Mvc;
using COMP306_Group6_Frontend.Models;

public class AirportController : Controller
{
    private readonly AirportService _service;
    public AirportController(AirportService service) => _service = service;

    public async Task<IActionResult> Index() =>
        View(await _service.GetAllAsync());

    public async Task<IActionResult> Details(string code)
    {
        var item = await _service.GetByIdAsync(code);
        if (item == null) return NotFound();
        return View(item);
    }

    public IActionResult Create() => View();

    [HttpPost]
    public async Task<IActionResult> Create(AirportCreateDto dto)
    {
        if (!ModelState.IsValid) return View(dto);
        await _service.CreateAsync(dto);
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Edit(string code)
    {
        var item = await _service.GetByIdAsync(code);
        if (item == null) return NotFound();
        var updateDto = new AirportUpdateDto
        {
            AirportName = item.AirportName,
            City = item.City,
            Country = item.Country,
            Timezone = item.Timezone,
            NumberOfTerminals = item.NumberOfTerminals
        };
        ViewBag.AirportCode = item.AirportCode;
        return View(updateDto);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(string code, AirportUpdateDto dto)
    {
        if (!ModelState.IsValid) return View(dto);
        await _service.PutAsync(code, dto);
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Patch(string code)
    {
        var item = await _service.GetByIdAsync(code);
        if (item == null) return NotFound();
        ViewBag.AirportCode = code;
        return View(new AirportPatchDto());
    }

    [HttpPost]
    public async Task<IActionResult> Patch(string code, AirportPatchDto dto)
    {
        if (!ModelState.IsValid) return View(dto);
        await _service.PatchAsync(code, dto);
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Delete(string code)
    {
        await _service.DeleteAsync(code);
        return RedirectToAction("Index");
    }
}
