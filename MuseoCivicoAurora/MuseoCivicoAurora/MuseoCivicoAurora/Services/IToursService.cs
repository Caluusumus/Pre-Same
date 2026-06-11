using MuseoCivicoAurora.shared;

namespace MuseoCivicoAurora.Service
{
    public interface IToursService
    {
        Task AddTourAsync(Tour tour);
        Task DeleteTourByIdAsync(Guid id);
        Task<Tour?> GetTourByIdAsync(Guid id);
        Task<IEnumerable<Tour>> GetToursAsync();
        Task UpdateTourAsync(Tour tour);
    }
}