using ClassModels;
using Dapper;
using MySqlConnector;

namespace MuseoCivicoAurora.Service;

public class OpereService : IOpereService
{
    private readonly string _connectionString;

    public OpereService(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection")
            ?? throw new Exception("Errore nella connessione al database.");
    }

    public async Task<IEnumerable<Opera>> GetOpereAsync()
    {
        await using var connection = new MySqlConnection(_connectionString);
        const string query = """
            SELECT 
            	Id,
                Titolo,
                Autore,
                Anno,
                Tipologia,
                Descrizione,
                Img,
                IdMostra
            FROM
            	opere;
            """;
        return await connection.QueryAsync<Opera>(query);
    }

    public async Task<Opera?> GetOperaByIdAsync(Guid id)
    {
        await using var connection = new MySqlConnection(_connectionString);
        const string query = """
            SELECT 
            	Id,
                Titolo,
                Autore,
                Anno,
                Tipologia,
                Descrizione,
                Img,
                IdMostra
            FROM
            	opere
            WHERE
                Id = @id;
            """;
        return await connection.QueryFirstOrDefaultAsync<Opera>(query, new { id });
    }

    public async Task AddOperaAsync(Opera opera)
    {
        await using var connection = new MySqlConnection(_connectionString);
        const string query = """
            INSERT INTO 
                opere
                (
                    Id,
                    Titolo,
                    Autore,
                    Anno,
                    Tipologia,
                    Descrizione,
                    Img,
                    IdMostra
                )
            VALUES 
                (
                    @Id,
                    @Titolo,
                    @Autore,
                    @Anno,
                    @Tipologia,
                    @Descrizione,
                    @Img,
                    @IdMostra
                );
            """;
        await connection.ExecuteAsync(query, opera);
    }

    public async Task UpdateOperaAsync(Opera opera)
    {
        await using var connection = new MySqlConnection(_connectionString);
        const string query = """
            UPDATE 
                opere
            SET
                Titolo = @Titolo,
                Autore = @Autore,
                Anno = @Anno,
                Tipologia = @Tipologia,
                Descrizione = @Decrizione,
                Img = @Img,
                IdMostra = @IdMostra
            WHERE 
                Id = @Id;
            """;
        await connection.ExecuteAsync(query, opera);
    }

    public async Task DeleteOperaByIdAsync(Guid id)
    {
        await using var connection = new MySqlConnection(_connectionString);
        const string query = """
            DELETE 
            FROM 
                opere
            WHERE 
                Id = @id;
            """;
        await connection.ExecuteAsync(query, new { id });
    }
}
