using System.Net.Http.Json;
using ClassModels;

namespace MuseoCivicoAurora.Client.Services;

public class TicketApiClient
{
    private readonly HttpClient _httpClient;
    public TicketApiClient(HttpClient httpClient) => _httpClient = httpClient;

    public async Task<List<Ticket>?> GetTicketsAsync() => await _httpClient.GetFromJsonAsync<List<Ticket>>("/api/tickets");
    public async Task<Ticket?> GetTicketByIdAsync(Guid id) => await _httpClient.GetFromJsonAsync<Ticket>($"/api/tickets/{id}");

    public async Task CreateTicketAsync(Ticket ticket)
    {
        ticket.Id = Guid.NewGuid();
        ticket.CreatedAt = DateTime.Now;
        await _httpClient.PostAsJsonAsync("/api/tickets", ticket);
    }

    public async Task UpdateTicketAsync(Guid id, Ticket ticket) => await _httpClient.PutAsJsonAsync($"/api/tickets/{id}", ticket);
    public async Task DeleteTicketAsync(Guid id) => await _httpClient.DeleteAsync($"/api/tickets/{id}");
}