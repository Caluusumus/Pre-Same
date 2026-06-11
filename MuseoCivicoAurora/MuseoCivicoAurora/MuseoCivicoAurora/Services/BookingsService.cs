using MuseoCivicoAurora.shared;
using Dapper;
using MySqlConnector;

namespace MuseoCivicoAurora.Service;

public class BookingsService : IBookingsService
{
    private readonly string _connectionString;

    public BookingsService(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection")
            ?? throw new Exception("Database connection error.");
    }

    public async Task<IEnumerable<Booking>> GetBookingsAsync()
    {
        await using var connection = new MySqlConnection(_connectionString);
        const string query = """
            SELECT 
                Id,
                FirstName,
                LastName,
                Email,
                ParticipantsCount,
                TourId,
                CreatedAt,
                Status
            FROM
                bookings;
            """;
        return await connection.QueryAsync<Booking>(query);
    }

    public async Task<Booking?> GetBookingByIdAsync(Guid id)
    {
        await using var connection = new MySqlConnection(_connectionString);
        const string query = """
            SELECT 
                Id,
                FirstName,
                LastName,
                Email,
                ParticipantsCount,
                TourId,
                CreatedAt,
                Status
            FROM
                bookings
            WHERE
                Id = @id;
            """;
        return await connection.QueryFirstOrDefaultAsync<Booking>(query, new { id });
    }

    public async Task AddBookingAsync(Booking booking)
    {
        await using var connection = new MySqlConnection(_connectionString);
        const string query = """
            INSERT INTO 
                bookings
                (
                    Id,
                    FirstName,
                    LastName,
                    Email,
                    ParticipantsCount,
                    TourId,
                    CreatedAt,
                    Status
                )
            VALUES 
                (
                    @Id,
                    @FirstName,
                    @LastName,
                    @Email,
                    @ParticipantsCount,
                    @TourId,
                    @CreatedAt,
                    @Status
                );
            """;
        await connection.ExecuteAsync(query, booking);
    }

    public async Task UpdateBookingAsync(Booking booking)
    {
        await using var connection = new MySqlConnection(_connectionString);
        const string query = """
            UPDATE 
                bookings
            SET
                FirstName = @FirstName,
                LastName = @LastName,
                Email = @Email,
                ParticipantsCount = @ParticipantsCount,
                TourId = @TourId,
                CreatedAt = @CreatedAt,
                Status = @Status
            WHERE 
                Id = @Id;
            """;
        await connection.ExecuteAsync(query, booking);
    }

    public async Task DeleteBookingByIdAsync(Guid id)
    {
        await using var connection = new MySqlConnection(_connectionString);
        const string query = """
            DELETE 
            FROM 
                bookings
            WHERE 
                Id = @id;
            """;
        await connection.ExecuteAsync(query, new { id });
    }
}