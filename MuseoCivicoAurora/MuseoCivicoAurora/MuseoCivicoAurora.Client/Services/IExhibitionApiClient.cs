using MuseoCivicoAurora.shared;

namespace MuseoCivicoAurora.Client.Services
{
    public interface IExhibitionApiClient
    {
        Task CreateExhibitionAsync(Exhibition exhibition);
        Task DeleteExhibitionAsync(Guid id);
        Task<Exhibition?> GetExhibitionByIdAsync(Guid id);
        Task<List<Exhibition>?> GetExhibitionsAsync();
        Task UpdateExhibitionAsync(Guid id, Exhibition exhibition);
    }
}