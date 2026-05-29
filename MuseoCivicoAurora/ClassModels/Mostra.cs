namespace ClassModels;

public class Mostra
{
    public Guid Id { get; set; }
    public string Titolo { get; set; } = default!;
    public string? Descrizione { get; set; }
    public DateTime DataInizio { get; set; }
    public DateTime DataFine { get; set; }
    public string? Img { get; set; }
    public string Stato { get; set; } = default!;
}
