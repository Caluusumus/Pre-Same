using ClassModels;
using Dapper;
using MySqlConnector;

namespace MuseoCivicoAurora.Service;

public class MostreService : IMostreService
{
    private readonly string _connectionString;

    public MostreService(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection")
            ?? throw new Exception("Errore nella connessione al database.");
    }

    public async Task<IEnumerable<Mostra>> GetMostreAsync()
    {
        await using var connection = new MySqlConnection(_connectionString);
        const string query = """
            SELECT 
            	Id,
                Titolo,
                Descrizione,
                DataInizio,
                DataFine,
                Img,
                Stato
            FROM
            	mostre;
            """;
        return await connection.QueryAsync<Mostra>(query);
    }

    public async Task<Mostra?> GetMostraByIdAsync(Guid id)
    {
        await using var connection = new MySqlConnection(_connectionString);
        const string query = """
            SELECT 
                Id,
                Titolo,
                Descrizione,
                DataInizio,
                DataFine,
                Img,
                Stato
            FROM
            	mostre
            WHERE
                Id = @id;
            """;
        return await connection.QueryFirstOrDefaultAsync<Mostra>(query, new { id });
    }

    public async Task AddMostraAsync(Mostra mostra)
    {
        await using var connection = new MySqlConnection(_connectionString);
        const string query = """
            INSERT INTO 
                mostre
                (
                    Id,
                    Titolo,
                    Descrizione,
                    DataInizio,
                    DataFine,
                    Img,
                    Stato
                )
            VALUES 
                (
                    @Id,
                    @Titolo,
                    @Descrizione,
                    @DataInizio,
                    @DataFine,
                    @Img,
                    @Stato
                );
            """;
        await connection.ExecuteAsync(query, mostra);
    }

    public async Task UpdateMostraAsync(Mostra mostra)
    {
        await using var connection = new MySqlConnection(_connectionString);
        const string query = """
            UPDATE 
                mostre
            SET
                Titolo = @Titolo,
                Descrizione = @Descrizione,
                DataInizio = @DataInzio,
                DataFine = @DataFine,
                Img = @Img,
                Stato = @Stato
            WHERE 
                Id = @Id;
            """;
        await connection.ExecuteAsync(query, mostra);
    }

    public async Task DeleteMostraByIdAsync(Guid id)
    {
        await using var connection = new MySqlConnection(_connectionString);
        const string query = """
            DELETE 
            FROM 
                mostre
            WHERE 
                Id = @id;
            """;
        await connection.ExecuteAsync(query, new { id });
    }
}