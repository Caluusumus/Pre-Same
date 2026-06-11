using MuseoCivicoAurora.shared;
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
        var exhibition = await connection.QueryFirstOrDefaultAsync<Exhibition>(query, new { id });

        if (exhibition != null)
        {
            const string queryOpere = """
            SELECT Id, Title, Artist, Year, Type, Description, Image, ExhibitionId
            FROM artworks a
            INNER JOIN exhibition_artworks ea ON a.Id = ea.ArtworkId
            WHERE ea.ExhibitionId = @id;
        """;

            var artworks = await connection.QueryAsync<Artwork>(queryOpere, new { id });
            exhibition.Artworks = artworks.ToList();
        }

        return exhibition;
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

    public async Task AddArtworkToExhibitionAsync(Guid exhibitionId, Guid artworkId)
    {
        await using var connection = new MySqlConnection(_connectionString);

        // INSERT IGNORE previene errori se il collegamento esiste già
        const string sql = """
        INSERT IGNORE INTO exhibition_artworks (ExhibitionId, ArtworkId) 
        VALUES (@exhibitionId, @artworkId);
    """;

        await connection.ExecuteAsync(sql, new { exhibitionId, artworkId });
    }

    public async Task RemoveArtworkFromExhibitionAsync(Guid exhibitionId, Guid artworkId)
    {
        await using var connection = new MySqlConnection(_connectionString);

        const string sql = """
        DELETE FROM exhibition_artworks 
        WHERE ExhibitionId = @exhibitionId AND ArtworkId = @artworkId;
    """;

        await connection.ExecuteAsync(sql, new { exhibitionId, artworkId });
    }
}