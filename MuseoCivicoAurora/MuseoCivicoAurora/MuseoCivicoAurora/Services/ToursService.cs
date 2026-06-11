using MuseoCivicoAurora.shared;
using Dapper;
using MySqlConnector;

namespace MuseoCivicoAurora.Service;

public class ToursService : IToursService
{
    private readonly string _connectionString;

    public ToursService(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection")
            ?? throw new Exception("Database connection error.");
    }

    public async Task<IEnumerable<Tour>> GetToursAsync()
    {
        await using var connection = new MySqlConnection(_connectionString);
        const string query = """
            SELECT 
                Id,
                Title,
                Description,
                DateTime,
                Duration,
                Guide,
                ParticipantsCount,
                ExhibitionId
            FROM
                tours;
            """;
        return await connection.QueryAsync<Tour>(query);
    }

    public async Task<Tour?> GetTourByIdAsync(Guid id)
    {
        await using var connection = new MySqlConnection(_connectionString);
        const string query = """
            SELECT 
                Id,
                Title,
                Description,
                DateTime,
                Duration,
                Guide,
                ParticipantsCount,
                ExhibitionId
            FROM
                tours
            WHERE
                Id = @id;
            """;
        return await connection.QueryFirstOrDefaultAsync<Tour>(query, new { id });
    }

    public async Task AddTourAsync(Tour tour)
    {
        await using var connection = new MySqlConnection(_connectionString);
        const string query = """
            INSERT INTO 
                tours
                (
                    Id,
                    Title,
                    Description,
                    DateTime,
                    Duration,
                    Guide,
                    ParticipantsCount,
                    ExhibitionId
                )
            VALUES 
                (
                    @Id,
                    @Title,
                    @Description,
                    @DateTime,
                    @Duration,
                    @Guide,
                    @ParticipantsCount,
                    @ExhibitionId
                );
            """;
        await connection.ExecuteAsync(query, tour);
    }

    public async Task UpdateTourAsync(Tour tour)
    {
        await using var connection = new MySqlConnection(_connectionString);
        const string query = """
            UPDATE 
                tours
            SET
                Title = @Title,
                Description = @Description,
                DateTime = @DateTime,
                Duration = @Duration,
                Guide = @Guide,
                ParticipantsCount = @ParticipantsCount,
                ExhibitionId = @ExhibitionId
            WHERE 
                Id = @Id;
            """;
        await connection.ExecuteAsync(query, tour);
    }

    public async Task DeleteTourByIdAsync(Guid id)
    {
        await using var connection = new MySqlConnection(_connectionString);
        const string query = """
            DELETE 
            FROM 
                tours
            WHERE 
                Id = @id;
            """;
        await connection.ExecuteAsync(query, new { id });
    }
}