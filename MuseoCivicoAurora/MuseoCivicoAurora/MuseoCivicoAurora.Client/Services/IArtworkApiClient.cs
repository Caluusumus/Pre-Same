using MuseoCivicoAurora.shared;

namespace MuseoCivicoAurora.Client.Services
{
    public interface IArtworkApiClient
    {
        Task CreateArtworkAsync(Artwork artwork);
        Task DeleteArtworkAsync(Guid id);
        Task<Artwork?> GetArtworkByIdAsync(Guid id);
        Task<List<Artwork>?> GetArtworksAsync();
        Task UpdateArtworkAsync(Guid id, Artwork artwork);
    }
}