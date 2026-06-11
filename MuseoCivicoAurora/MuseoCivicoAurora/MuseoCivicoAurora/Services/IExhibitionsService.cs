using MuseoCivicoAurora.shared;

namespace MuseoCivicoAurora.Service
{
    public interface IExhibitionsService
    {
        Task AddExhibitionAsync(Exhibition exhibition);
        Task DeleteExhibitionByIdAsync(Guid id);
        Task<Exhibition?> GetExhibitionByIdAsync(Guid id);
        Task<IEnumerable<Exhibition>> GetExhibitionsAsync();
        Task UpdateExhibitionAsync(Exhibition exhibition);
        Task AddArtworkToExhibitionAsync(Guid exhibitionId, Guid artworkId);
        Task RemoveArtworkFromExhibitionAsync(Guid exhibitionId, Guid artworkId);
    }
}