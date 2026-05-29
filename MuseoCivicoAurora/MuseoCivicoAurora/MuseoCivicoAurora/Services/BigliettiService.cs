using ClassModels;
using Dapper;
using MySqlConnector;

namespace MuseoCivicoAurora.Service;

public class BigliettiService : IBigliettiService
{
    private readonly string _connectionString;

    public BigliettiService(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection")
            ?? throw new Exception("Errore nella connessione al database.");
    }

    public async Task<IEnumerable<Mostra>> GetBigliettiAsync()
    {
        await using var connection = new MySqlConnection(_connectionString);
        const string query = """
            SELECT 
            	Id,
                Nome,
                Cognome,
                Email,
                Tipologia,
                Quantita,
                Totale,
                CreatedAt,
                IdMostra,
                IdVisita
            FROM
            	Biglietti;
            """;
        return await connection.QueryAsync<Mostra>(query);
    }

    public async Task<Mostra?> GetBigliettoByIdAsync(Guid id)
    {
        await using var connection = new MySqlConnection(_connectionString);
        const string query = """
            SELECT 
                Id,
                Nome,
                Cognome,
                Email,
                Tipologia,
                Quantita,
                Totale,
                CreatedAt,
                IdMostra,
                IdVisita
            FROM
            	Biglietti
            WHERE
                Id = @id;
            """;
        return await connection.QueryFirstOrDefaultAsync<Mostra>(query, new { id });
    }

    public async Task AddBigliettoAsync(Mostra mostra)
    {
        await using var connection = new MySqlConnection(_connectionString);
        const string query = """
            INSERT INTO 
                Biglietti
                (
                    Id,
                    Nome,
                    Cognome,
                    Email,
                    Tipologia,
                    Quantita,
                    Totale,
                    CreatedAt,
                    IdMostra,
                    IdVisita
                )
            VALUES 
                (
                    @Id,
                    @Nome,
                    @Cognome,
                    @Email,
                    @Tipologia,
                    @Quantita,
                    @Totale,
                    @CreatedAt,
                    @IdMostra,
                    @IdVisita
                );
            """;
        await connection.ExecuteAsync(query, mostra);
    }

    public async Task UpdateBigliettoAsync(Mostra mostra)
    {
        await using var connection = new MySqlConnection(_connectionString);
        const string query = """
            UPDATE 
                Biglietti
            SET
                Nome = @Nome,
                Cognome = @Cognome,
                Email = @Email,
                Tipologia = @Tipologia,
                Quantita = @Quantita,
                Totale = @Totale,
                CreatedAt = @CreatedAt,
                IdMostra = @IdMostra,
                IdVisita = @IdVisita
            WHERE 
                Id = @Id;
            """;
        await connection.ExecuteAsync(query, mostra);
    }

    public async Task DeleteBigliettoByIdAsync(Guid id)
    {
        await using var connection = new MySqlConnection(_connectionString);
        const string query = """
            DELETE 
            FROM 
                Biglietti
            WHERE 
                Id = @id;
            """;
        await connection.ExecuteAsync(query, new { id });
    }
}
