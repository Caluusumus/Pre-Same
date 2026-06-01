using System.Net.Http.Json;
using ClassModels;

namespace MuseoCivicoAurora.Client.Services;

public class ExhibitionApiClient
{
    private readonly HttpClient _httpClient;

    public ExhibitionApiClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<Exhibition>?> GetExhibitionsAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<Exhibition>>("api/exhibitions");
    }

    public async Task<Exhibition?> GetExhibitionByIdAsync(Guid id)
    {
        return await _httpClient.GetFromJsonAsync<Exhibition>($"api/exhibitions/{id}");
    }

    public async Task CreateExhibitionAsync(Exhibition exhibition)
    {
        exhibition.Id = Guid.NewGuid(); // Genera un nuovo Guid se non è gestito dal DB
        await _httpClient.PostAsJsonAsync("api/exhibitions", exhibition);
    }

    public async Task UpdateExhibitionAsync(Guid id, Exhibition exhibition)
    {
        await _httpClient.PutAsJsonAsync($"api/exhibitions/{id}", exhibition);
    }

    public async Task DeleteExhibitionAsync(Guid id)
    {
        await _httpClient.DeleteAsync($"api/exhibitions/{id}");
    }

}