using MuseoCivicoAurora.shared;

namespace MuseoCivicoAurora.Service
{
    public interface ITicketsService
    {
        Task AddTicketAsync(Ticket ticket);
        Task DeleteTicketByIdAsync(Guid id);
        Task<IEnumerable<Ticket>> GetTicketsAsync();
        Task<Ticket?> GetTicketByIdAsync(Guid id);
        Task UpdateTicketAsync(Ticket ticket);
    }
}