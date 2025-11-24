using System.Net.Http.Json;
using COMP306_Group6_Frontend.Models;

public class FlightService
{
    private readonly HttpClient _httpClient;

    public FlightService(IHttpClientFactory factory)
    {
        _httpClient = factory.CreateClient("FlightApi");
    }

    public async Task<List<FlightReadDto>> GetAllAsync() =>
        await _httpClient.GetFromJsonAsync<List<FlightReadDto>>("api/flights") ?? new();

    public async Task<FlightReadDto?> GetByIdAsync(string id) =>
        await _httpClient.GetFromJsonAsync<FlightReadDto>($"api/flights/{id}");

    public async Task<FlightReadDto?> CreateAsync(FlightCreateDto dto)
    {
        var resp = await _httpClient.PostAsJsonAsync("api/flights", dto);
        resp.EnsureSuccessStatusCode();
        return await resp.Content.ReadFromJsonAsync<FlightReadDto>();
    }

    public async Task<FlightReadDto?> PutAsync(string id, FlightUpdateDto dto)
    {
        var resp = await _httpClient.PutAsJsonAsync($"api/flights/{id}", dto);
        resp.EnsureSuccessStatusCode();
        return await resp.Content.ReadFromJsonAsync<FlightReadDto>();
    }

    public async Task<FlightReadDto?> PatchAsync(string id, FlightPatchDto dto)
    {
        var req = new HttpRequestMessage(new HttpMethod("PATCH"), $"api/flights/{id}")
        { Content = JsonContent.Create(dto) };
        var resp = await _httpClient.SendAsync(req);
        resp.EnsureSuccessStatusCode();
        return await resp.Content.ReadFromJsonAsync<FlightReadDto>();
    }

    public async Task<bool> DeleteAsync(string id)
    {
        var resp = await _httpClient.DeleteAsync($"api/flights/{id}");
        return resp.IsSuccessStatusCode;
    }
}
