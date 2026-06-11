using MuseoCivicoAurora.shared;

namespace MuseoCivicoAurora.Client.Services
{
    public interface ITourApiClient
    {
        Task CreateTourAsync(Tour tour);
        Task DeleteTourAsync(Guid id);
        Task<Tour?> GetTourByIdAsync(Guid id);
        Task<List<Tour>?> GetToursAsync();
        Task UpdateTourAsync(Guid id, Tour tour);
    }
}