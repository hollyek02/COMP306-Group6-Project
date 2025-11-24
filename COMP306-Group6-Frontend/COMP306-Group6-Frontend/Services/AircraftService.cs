using System.Net.Http.Json;
using COMP306_Group6_Frontend.Models;

public class AircraftService
{
    private readonly HttpClient _httpClient;

    public AircraftService(IHttpClientFactory factory)
    {
        _httpClient = factory.CreateClient("FlightApi");
    }

    public async Task<List<AircraftReadDto>> GetAllAsync() =>
        await _httpClient.GetFromJsonAsync<List<AircraftReadDto>>("api/aircrafts") ?? new();

    public async Task<AircraftReadDto?> GetByIdAsync(string id) =>
        await _httpClient.GetFromJsonAsync<AircraftReadDto>($"api/aircrafts/{id}");

    public async Task<AircraftReadDto?> CreateAsync(AircraftCreateDto dto)
    {
        var resp = await _httpClient.PostAsJsonAsync("api/aircrafts", dto);
        resp.EnsureSuccessStatusCode();
        return await resp.Content.ReadFromJsonAsync<AircraftReadDto>();
    }

    public async Task<AircraftReadDto?> PutAsync(string id, AircraftUpdateDto dto)
    {
        var resp = await _httpClient.PutAsJsonAsync($"api/aircrafts/{id}", dto);
        resp.EnsureSuccessStatusCode();
        return await resp.Content.ReadFromJsonAsync<AircraftReadDto>();
    }

    public async Task<AircraftReadDto?> PatchAsync(string id, AircraftPatchDto dto)
    {
        var req = new HttpRequestMessage(new HttpMethod("PATCH"), $"api/aircrafts/{id}")
        { Content = JsonContent.Create(dto) };
        var resp = await _httpClient.SendAsync(req);
        resp.EnsureSuccessStatusCode();
        return await resp.Content.ReadFromJsonAsync<AircraftReadDto>();
    }

    public async Task<bool> DeleteAsync(string id)
    {
        var resp = await _httpClient.DeleteAsync($"api/aircrafts/{id}");
        return resp.IsSuccessStatusCode;
    }
}
