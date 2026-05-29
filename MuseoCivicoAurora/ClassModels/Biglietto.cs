namespace ClassModels;

public class Biglietto
{
    public Guid Id { get; set; }
    public string? Nome { get; set; }
    public string Email { get; set; } = default!;
    public string Tipologia { get; set; } = default!;
    public int Quantita { get; set; }
    public decimal Totale { get; set; }
    public DateTime? CreatedAt { get; set; }
    public Guid? IdMostra { get; set; }
    public Guid? IdVisita { get; set; }
}
