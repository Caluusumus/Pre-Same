using ClassModels;

namespace MuseoCivicoAurora.Service
{
    public interface IBookingsService
    {
        Task AddBookingAsync(Booking booking);
        Task DeleteBookingByIdAsync(Guid id);
        Task<Booking?> GetBookingByIdAsync(Guid id);
        Task<IEnumerable<Booking>> GetBookingsAsync();
        Task UpdateBookingAsync(Booking booking);
    }
}