namespace ClassModels;

public class Prenotazione
{
    public Guid Id { get; set; }
    public string? Nome { get; set; }
    public string? Cognome { get; set; }
    public string Email { get; set; } = default!;
    public int NumeroPartecipanti { get; set; }
    public Guid IdVisita { get; set; }
    public DateTime? CreatedAt { get; set; }
    public string Stato { get; set; } = default!;
}
