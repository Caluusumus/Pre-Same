using System.Net.Http.Json;
using ClassModels;

namespace MuseoCivicoAurora.Client.Services;

public class ArtworkApiClient
{
    private readonly HttpClient _httpClient;
    public ArtworkApiClient(HttpClient httpClient) => _httpClient = httpClient;

    public async Task<List<Artwork>?> GetArtworksAsync() => await _httpClient.GetFromJsonAsync<List<Artwork>>("/api/artworks");
    public async Task<Artwork?> GetArtworkByIdAsync(Guid id) => await _httpClient.GetFromJsonAsync<Artwork>($"/api/artworks/{id}");

    public async Task CreateArtworkAsync(Artwork artwork)
    {
        artwork.Id = Guid.NewGuid();
        await _httpClient.PostAsJsonAsync("/api/artworks", artwork);
    }

    public async Task UpdateArtworkAsync(Guid id, Artwork artwork) => await _httpClient.PutAsJsonAsync($"/api/artworks/{id}", artwork);
    public async Task DeleteArtworkAsync(Guid id) => await _httpClient.DeleteAsync($"/api/artworks/{id}");
}