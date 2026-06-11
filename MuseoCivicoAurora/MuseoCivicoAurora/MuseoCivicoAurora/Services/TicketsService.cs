using MuseoCivicoAurora.shared;
using Dapper;
using MySqlConnector;

namespace MuseoCivicoAurora.Service;

public class TicketsService : ITicketsService
{
    private readonly string _connectionString;

    public TicketsService(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection")
            ?? throw new Exception("Database connection error.");
    }

    public async Task<IEnumerable<Ticket>> GetTicketsAsync()
    {
        await using var connection = new MySqlConnection(_connectionString);
        const string query = """
            SELECT 
                Id,
                FirstName,
                LastName,
                Email,
                Type,
                Quantity,
                Total,
                CreatedAt,
                ExhibitionId,
                TourId
            FROM
                tickets;
            """;
        return await connection.QueryAsync<Ticket>(query);
    }

    public async Task<Ticket?> GetTicketByIdAsync(Guid id)
    {
        await using var connection = new MySqlConnection(_connectionString);
        const string query = """
            SELECT 
                Id,
                FirstName,
                LastName,
                Email,
                Type,
                Quantity,
                Total,
                CreatedAt,
                ExhibitionId,
                TourId
            FROM
                tickets
            WHERE
                Id = @id;
            """;
        return await connection.QueryFirstOrDefaultAsync<Ticket>(query, new { id });
    }

    public async Task AddTicketAsync(Ticket ticket)
    {
        await using var connection = new MySqlConnection(_connectionString);
        const string query = """
            INSERT INTO 
                tickets
                (
                    Id,
                    FirstName,
                    LastName,
                    Email,
                    Type,
                    Quantity,
                    Total,
                    CreatedAt,
                    ExhibitionId,
                    TourId
                )
            VALUES 
                (
                    @Id,
                    @FirstName,
                    @LastName,
                    @Email,
                    @Type,
                    @Quantity,
                    @Total,
                    @CreatedAt,
                    @ExhibitionId,
                    @TourId
                );
            """;
        await connection.ExecuteAsync(query, ticket);
    }

    public async Task UpdateTicketAsync(Ticket ticket)
    {
        await using var connection = new MySqlConnection(_connectionString);
        const string query = """
            UPDATE 
                tickets
            SET
                FirstName = @FirstName,
                LastName = @LastName,
                Email = @Email,
                Type = @Type,
                Quantity = @Quantity,
                Total = @Total,
                CreatedAt = @CreatedAt,
                ExhibitionId = @ExhibitionId,
                TourId = @TourId
            WHERE 
                Id = @Id;
            """;
        await connection.ExecuteAsync(query, ticket);
    }

    public async Task DeleteTicketByIdAsync(Guid id)
    {
        await using var connection = new MySqlConnection(_connectionString);
        const string query = """
            DELETE 
            FROM 
                tickets
            WHERE 
                Id = @id;
            """;
        await connection.ExecuteAsync(query, new { id });
    }
}