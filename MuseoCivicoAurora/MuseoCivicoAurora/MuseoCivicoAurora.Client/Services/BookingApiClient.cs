using System.Net.Http.Json;
using MuseoCivicoAurora.shared;

namespace MuseoCivicoAurora.Client.Services;

public class BookingApiClient : IBookingApiClient
{
    private readonly HttpClient _httpClient;
    public BookingApiClient(HttpClient httpClient) => _httpClient = httpClient;

    public async Task<List<Booking>?> GetBookingsAsync() => await _httpClient.GetFromJsonAsync<List<Booking>>("/api/bookings");
    public async Task<Booking?> GetBookingByIdAsync(Guid id) => await _httpClient.GetFromJsonAsync<Booking>($"/api/bookings/{id}");

    public async Task CreateBookingAsync(Booking booking)
    {
        booking.Id = Guid.NewGuid();
        booking.CreatedAt = DateTime.Now;
        await _httpClient.PostAsJsonAsync("/api/bookings", booking);
    }

    public async Task UpdateBookingAsync(Guid id, Booking booking) => await _httpClient.PutAsJsonAsync($"/api/bookings/{id}", booking);
    public async Task DeleteBookingAsync(Guid id) => await _httpClient.DeleteAsync($"/api/bookings/{id}");
}