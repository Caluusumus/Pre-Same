using System.Data;

namespace ClassModels;

public class Visita
{
    public Guid Id { get; set; }
    public string Titolo { get; set; } = default!;
    public string? Decrizione { get; set; }
    public DateTime? DataOra { get; set; }
    public TimeOnly? Durata { get; set; }
    public string? Guida { get; set; }
    public int? NumeroPartecipanti { get; set; }
    public Guid? IdMostra { get; set; }
}
