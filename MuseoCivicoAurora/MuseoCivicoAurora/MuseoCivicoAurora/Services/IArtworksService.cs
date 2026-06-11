using MuseoCivicoAurora.shared;

namespace MuseoCivicoAurora.Service
{
    public interface IArtworksService
    {
        Task AddArtworkAsync(Artwork artwork);
        Task DeleteArtworkByIdAsync(Guid id);
        Task<Artwork?> GetArtworkByIdAsync(Guid id);
        Task<IEnumerable<Artwork>> GetArtworksAsync();
        Task UpdateArtworkAsync(Artwork artwork);
    }
}