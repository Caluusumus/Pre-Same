using ClassModels;
using Dapper;
using MySqlConnector;

namespace MuseoCivicoAurora.Service;

public class ArtworksService : IArtworksService
{
    private readonly string _connectionString;

    public ArtworksService(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection")
            ?? throw new Exception("Database connection error.");
    }

    public async Task<IEnumerable<Artwork>> GetArtworksAsync()
    {
        await using var connection = new MySqlConnection(_connectionString);
        const string query = """
            SELECT 
                Id,
                Title,
                Artist,
                Year,
                Type,
                Description,
                Image,
                ExhibitionId
            FROM
                artworks;
            """;
        return await connection.QueryAsync<Artwork>(query);
    }

    public async Task<Artwork?> GetArtworkByIdAsync(Guid id)
    {
        await using var connection = new MySqlConnection(_connectionString);
        const string query = """
            SELECT 
                Id,
                Title,
                Artist,
                Year,
                Type,
                Description,
                Image,
                ExhibitionId
            FROM
                artworks
            WHERE
                Id = @id;
            """;
        return await connection.QueryFirstOrDefaultAsync<Artwork>(query, new { id });
    }

    public async Task AddArtworkAsync(Artwork artwork)
    {
        await using var connection = new MySqlConnection(_connectionString);
        const string query = """
            INSERT INTO 
                artworks
                (
                    Id,
                    Title,
                    Artist,
                    Year,
                    Type,
                    Description,
                    Image,
                    ExhibitionId
                )
            VALUES 
                (
                    @Id,
                    @Title,
                    @Artist,
                    @Year,
                    @Type,
                    @Description,
                    @Image,
                    @ExhibitionId
                );
            """;
        await connection.ExecuteAsync(query, artwork);
    }

    public async Task UpdateArtworkAsync(Artwork artwork)
    {
        await using var connection = new MySqlConnection(_connectionString);
        const string query = """
            UPDATE 
                artworks
            SET
                Title = @Title,
                Artist = @Artist,
                Year = @Year,
                Type = @Type,
                Description = @Description,
                Image = @Image,
                ExhibitionId = @ExhibitionId
            WHERE 
                Id = @Id;
            """;
        await connection.ExecuteAsync(query, artwork);
    }

    public async Task DeleteArtworkByIdAsync(Guid id)
    {
        await using var connection = new MySqlConnection(_connectionString);
        const string query = """
            DELETE 
            FROM 
                artworks
            WHERE 
                Id = @id;
            """;
        await connection.ExecuteAsync(query, new { id });
    }
}