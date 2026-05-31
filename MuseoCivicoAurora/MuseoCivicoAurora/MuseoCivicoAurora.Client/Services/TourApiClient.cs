using System.Net.Http.Json;
using ClassModels;

namespace MuseoCivicoAurora.Client.Services;

public class TourApiClient
{
    private readonly HttpClient _httpClient;
    public TourApiClient(HttpClient httpClient) => _httpClient = httpClient;

    public async Task<List<Tour>?> GetToursAsync() => await _httpClient.GetFromJsonAsync<List<Tour>>("/api/tours");
    public async Task<Tour?> GetTourByIdAsync(Guid id) => await _httpClient.GetFromJsonAsync<Tour>($"/api/tours/{id}");

    public async Task CreateTourAsync(Tour tour)
    {
        tour.Id = Guid.NewGuid();
        await _httpClient.PostAsJsonAsync("/api/tours", tour);
    }

    public async Task UpdateTourAsync(Guid id, Tour tour) => await _httpClient.PutAsJsonAsync($"/api/tours/{id}", tour);
    public async Task DeleteTourAsync(Guid id) => await _httpClient.DeleteAsync($"/api/tours/{id}");
}