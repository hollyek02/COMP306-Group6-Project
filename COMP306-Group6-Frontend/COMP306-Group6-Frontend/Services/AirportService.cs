using System.Net.Http.Json;
using COMP306_Group6_Frontend.Models;

public class AirportService
{
    private readonly HttpClient _httpClient;

    public AirportService(IHttpClientFactory factory)
    {
        _httpClient = factory.CreateClient("FlightApi");
    }

    public async Task<List<AirportReadDto>> GetAllAsync() =>
        await _httpClient.GetFromJsonAsync<List<AirportReadDto>>("api/airports") ?? new();

    public async Task<AirportReadDto?> GetByIdAsync(string code) =>
        await _httpClient.GetFromJsonAsync<AirportReadDto>($"api/airports/{code}");

    public async Task<AirportReadDto?> CreateAsync(AirportCreateDto dto)
    {
        var resp = await _httpClient.PostAsJsonAsync("api/airports", dto);
        resp.EnsureSuccessStatusCode();
        return await resp.Content.ReadFromJsonAsync<AirportReadDto>();
    }

    public async Task<AirportReadDto?> PutAsync(string code, AirportUpdateDto dto)
    {
        var resp = await _httpClient.PutAsJsonAsync($"api/airports/{code}", dto);
        resp.EnsureSuccessStatusCode();
        return await resp.Content.ReadFromJsonAsync<AirportReadDto>();
    }

    public async Task<AirportReadDto?> PatchAsync(string code, AirportPatchDto dto)
    {
        var req = new HttpRequestMessage(new HttpMethod("PATCH"), $"api/airports/{code}")
        { Content = JsonContent.Create(dto) };
        var resp = await _httpClient.SendAsync(req);
        resp.EnsureSuccessStatusCode();
        return await resp.Content.ReadFromJsonAsync<AirportReadDto>();
    }

    public async Task<bool> DeleteAsync(string code)
    {
        var resp = await _httpClient.DeleteAsync($"api/airports/{code}");
        return resp.IsSuccessStatusCode;
    }
}
