using System.Net.Http.Json;
using COMP306_Group6_Frontend.Models;

public class BookingService
{
    private readonly HttpClient _httpClient;
    public BookingService(IHttpClientFactory factory)
    {
        _httpClient = factory.CreateClient("FlightApi");
    }

    public async Task<List<BookingReadDto>> GetAllAsync()
        => await _httpClient.GetFromJsonAsync<List<BookingReadDto>>("api/bookings") ?? new();

    public async Task<BookingReadDto?> GetByIdAsync(string id)
        => await _httpClient.GetFromJsonAsync<BookingReadDto>($"api/bookings/{id}");

    public async Task<BookingReadDto?> CreateAsync(BookingCreateDto dto)
    {
        var resp = await _httpClient.PostAsJsonAsync("api/bookings", dto);
        resp.EnsureSuccessStatusCode();
        return await resp.Content.ReadFromJsonAsync<BookingReadDto>();
    }

    public async Task<BookingReadDto?> PutAsync(string id, BookingCreateDto dto)
    {
        var resp = await _httpClient.PutAsJsonAsync($"api/bookings/{id}", dto);
        resp.EnsureSuccessStatusCode();
        return await resp.Content.ReadFromJsonAsync<BookingReadDto>();
    }

    public async Task<BookingReadDto?> PatchAsync(string id, BookingPatchDto dto)
    {
        var req = new HttpRequestMessage(new HttpMethod("PATCH"), $"api/bookings/{id}")
        { Content = JsonContent.Create(dto) };
        var resp = await _httpClient.SendAsync(req);
        resp.EnsureSuccessStatusCode();
        return await resp.Content.ReadFromJsonAsync<BookingReadDto>();
    }

    public async Task<bool> DeleteAsync(string id)
    {
        var resp = await _httpClient.DeleteAsync($"api/bookings/{id}");
        return resp.IsSuccessStatusCode;
    }

    public async Task<List<BookingReadDto>> GetByEmailAsync(string email)
        => await _httpClient.GetFromJsonAsync<List<BookingReadDto>>($"api/bookings/byemail/{email}") ?? new();
}
