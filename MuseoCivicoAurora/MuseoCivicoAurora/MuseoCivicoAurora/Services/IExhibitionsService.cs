using ClassModels;

namespace MuseoCivicoAurora.Service
{
    public interface IExhibitionsService
    {
        Task AddExhibitionAsync(Exhibition exhibition);
        Task DeleteExhibitionByIdAsync(Guid id);
        Task<Exhibition?> GetExhibitionByIdAsync(Guid id);
        Task<IEnumerable<Exhibition>> GetExhibitionsAsync();
        Task UpdateExhibitionAsync(Exhibition exhibition);
    }
}