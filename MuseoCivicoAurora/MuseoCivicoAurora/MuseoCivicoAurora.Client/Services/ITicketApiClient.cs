using MuseoCivicoAurora.shared;

namespace MuseoCivicoAurora.Client.Services
{
    public interface ITicketApiClient
    {
        Task CreateTicketAsync(Ticket ticket);
        Task DeleteTicketAsync(Guid id);
        Task<Ticket?> GetTicketByIdAsync(Guid id);
        Task<List<Ticket>?> GetTicketsAsync();
        Task UpdateTicketAsync(Guid id, Ticket ticket);
    }
}