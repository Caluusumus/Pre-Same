using ClassModels;
using Dapper;
using MySqlConnector;

namespace MuseoCivicoAurora.Service;

public class PrenotazioniService : IPrenotazioniService
{
    private readonly string _connectionString;

    public PrenotazioniService(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection")
            ?? throw new Exception("Errore nella connessione al database.");
    }

    public async Task<IEnumerable<Prenotazione>> GetPrenotazioniAsync()
    {
        await using var connection = new MySqlConnection(_connectionString);
        const string query = """
            SELECT 
            	Id,
                Nome,
                Cognome,
                Email,
                NumeroPartecipanti,
                IdVisita,
                CreatedAt,
                Stato
            FROM
            	Prenotazioni;
            """;
        return await connection.QueryAsync<Prenotazione>(query);
    }

    public async Task<Prenotazione?> GetPrenotazioneByIdAsync(Guid id)
    {
        await using var connection = new MySqlConnection(_connectionString);
        const string query = """
            SELECT 
            	Id,
                Nome,
                Cognome,
                Email,
                NumeroPartecipanti,
                IdVisita,
                CreatedAt,
                Stato
            FROM
            	Prenotazioni
            WHERE
                Id = @id;
            """;
        return await connection.QueryFirstOrDefaultAsync<Prenotazione>(query, new { id });
    }

    public async Task AddPrenotazioneAsync(Prenotazione Prenotazione)
    {
        await using var connection = new MySqlConnection(_connectionString);
        const string query = """
            INSERT INTO 
                Prenotazioni
                (
                    Id,
                    Nome,
                    Cognome,
                    Email,
                    NumeroPartecipanti,
                    IdVisita,
                    CreatedAt,
                    Stato
                )
            VALUES 
                (
                    @Id,
                    @Nome,
                    @Cognome,
                    @Email,
                    @NumeroPartecipanti,
                    @IdVisita,
                    @CreatedAt,
                    @Stato
                );
            """;
        await connection.ExecuteAsync(query, Prenotazione);
    }

    public async Task UpdatePrenotazioneAsync(Prenotazione Prenotazione)
    {
        await using var connection = new MySqlConnection(_connectionString);
        const string query = """
            UPDATE 
                Prenotazioni
            SET
                Nome = @Nome,
                Cognome = @Cognome,
                Email = @Email,
                NumeroPartecipanti = @NumeroPartecipanti,
                IdVisita = @IdVisita,
                CreatedAt = @CreatedAt,
                Stato = @Stato
            WHERE 
                Id = @Id;
            """;
        await connection.ExecuteAsync(query, Prenotazione);
    }

    public async Task DeletePrenotazioneByIdAsync(Guid id)
    {
        await using var connection = new MySqlConnection(_connectionString);
        const string query = """
            DELETE 
            FROM 
                Prenotazioni
            WHERE 
                Id = @id;
            """;
        await connection.ExecuteAsync(query, new { id });
    }
}
