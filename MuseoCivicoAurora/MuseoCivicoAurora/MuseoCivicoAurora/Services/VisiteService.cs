using ClassModels;
using Dapper;
using MySqlConnector;

namespace MuseoCivicoAurora.Service;

public class VisiteService : IVisiteService
{
    private readonly string _connectionString;

    public VisiteService(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection")
            ?? throw new Exception("Errore nella connessione al database.");
    }

    public async Task<IEnumerable<Visita>> GetVisiteAsync()
    {
        await using var connection = new MySqlConnection(_connectionString);
        const string query = """
            SELECT 
            	Id,
                Titolo,
                Descrizione,
                DataOra,
                Durata,
                Guida,
                NumeroPartecipanti
            FROM
            	Visite;
            """;
        return await connection.QueryAsync<Visita>(query);
    }

    public async Task<Visita?> GetVisitaByIdAsync(Guid id)
    {
        await using var connection = new MySqlConnection(_connectionString);
        const string query = """
            SELECT 
            	Id,
                Titolo,
                Descrizione,
                DataOra,
                Durata,
                Guida,
                NumeroPartecipanti
            FROM
            	Visite
            WHERE
                Id = @id;
            """;
        return await connection.QueryFirstOrDefaultAsync<Visita>(query, new { id });
    }

    public async Task AddVisitaAsync(Visita Visita)
    {
        await using var connection = new MySqlConnection(_connectionString);
        const string query = """
            INSERT INTO 
                Visite
                (
                    Id,
                    Titolo,
                    Descrizione,
                    DataOra,
                    Durata,
                    Guida,
                    NumeroPartecipanti
                )
            VALUES 
                (
                    @Id,
                    @Titolo,
                    @Descrizione,
                    @DataOra,
                    @Durata,
                    @Guida,
                    @NumeroPartecipanti
                );
            """;
        await connection.ExecuteAsync(query, Visita);
    }

    public async Task UpdateVisitaAsync(Visita Visita)
    {
        await using var connection = new MySqlConnection(_connectionString);
        const string query = """
            UPDATE 
                Visite
            SET
                Titolo = @Titolo,
                Descrizione = @Descrizione,
                DataOra = @DataOra,
                Durata = @Durata,
                Guida = @Guida,
                NumeroPartecipanti = @NumeroPartecipanti
            WHERE 
                Id = @Id;
            """;
        await connection.ExecuteAsync(query, Visita);
    }

    public async Task DeleteVisitaByIdAsync(Guid id)
    {
        await using var connection = new MySqlConnection(_connectionString);
        const string query = """
            DELETE 
            FROM 
                Visite
            WHERE 
                Id = @id;
            """;
        await connection.ExecuteAsync(query, new { id });
    }
}
