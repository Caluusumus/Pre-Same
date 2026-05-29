using ClassModels;
using Dapper;
using MySqlConnector;

namespace MuseoCivicoAurora.Service;

public class ExhibitionsService : IExhibitionsService
{
    private readonly string _connectionString;

    public ExhibitionsService(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection")
            ?? throw new Exception("Database connection error.");
    }

    public async Task<IEnumerable<Exhibition>> GetExhibitionsAsync()
    {
        await using var connection = new MySqlConnection(_connectionString);
        const string query = """
            SELECT 
                Id,
                Title,
                Description,
                StartDate,
                EndDate,
                Image,
                Status
            FROM
                exhibitions;
            """;
        return await connection.QueryAsync<Exhibition>(query);
    }

    public async Task<Exhibition?> GetExhibitionByIdAsync(Guid id)
    {
        await using var connection = new MySqlConnection(_connectionString);
        const string query = """
            SELECT 
                Id,
                Title,
                Description,
                StartDate,
                EndDate,
                Image,
                Status
            FROM
                exhibitions
            WHERE
                Id = @id;
            """;
        return await connection.QueryFirstOrDefaultAsync<Exhibition>(query, new { id });
    }

    public async Task AddExhibitionAsync(Exhibition exhibition)
    {
        await using var connection = new MySqlConnection(_connectionString);
        const string query = """
            INSERT INTO 
                exhibitions
                (
                    Id,
                    Title,
                    Description,
                    StartDate,
                    EndDate,
                    Image,
                    Status
                )
            VALUES 
                (
                    @Id,
                    @Title,
                    @Description,
                    @StartDate,
                    @EndDate,
                    @Image,
                    @Status
                );
            """;
        await connection.ExecuteAsync(query, exhibition);
    }

    public async Task UpdateExhibitionAsync(Exhibition exhibition)
    {
        await using var connection = new MySqlConnection(_connectionString);
        const string query = """
            UPDATE 
                exhibitions
            SET
                Title = @Title,
                Description = @Description,
                StartDate = @StartDate,
                EndDate = @EndDate,
                Image = @Image,
                Status = @Status
            WHERE 
                Id = @Id;
            """;
        await connection.ExecuteAsync(query, exhibition);
    }

    public async Task DeleteExhibitionByIdAsync(Guid id)
    {
        await using var connection = new MySqlConnection(_connectionString);
        const string query = """
            DELETE 
            FROM 
                exhibitions
            WHERE 
                Id = @id;
            """;
        await connection.ExecuteAsync(query, new { id });
    }
}