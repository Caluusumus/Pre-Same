using MuseoCivicoAurora.shared;

namespace MuseoCivicoAurora.Client.Services
{
    public interface IBookingApiClient
    {
        Task CreateBookingAsync(Booking booking);
        Task DeleteBookingAsync(Guid id);
        Task<Booking?> GetBookingByIdAsync(Guid id);
        Task<List<Booking>?> GetBookingsAsync();
        Task UpdateBookingAsync(Guid id, Booking booking);
    }
}