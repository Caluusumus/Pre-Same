namespace ClassModels;

public class Opera
{
    public Guid Id { get; set; }
    public string Titolo { get; set; } = default!;
    public string? Autore {  get; set; }
    public DateOnly? Anno { get; set; }
    public string? Descrizione { get; set; }
    public string? Tipologia { get; set; }
    public string? Img { get; set; }
    public Guid? IdMostra { get; set; }
}
