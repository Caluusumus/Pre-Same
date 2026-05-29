using ClassModels;

namespace MuseoCivicoAurora.Service
{
    public interface IVisiteService
    {
        Task AddVisitaAsync(Visita Visita);
        Task DeleteVisitaByIdAsync(Guid id);
        Task<Visita?> GetVisitaByIdAsync(Guid id);
        Task<IEnumerable<Visita>> GetVisiteAsync();
        Task UpdateVisitaAsync(Visita Visita);
    }
}