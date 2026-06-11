namespace MuseoCivicoAurora.shared;

public class Artwork
{
    public Guid Id { get; set; }
    public string Title { get; set; } = default!;
    public string? Artist { get; set; }
    public DateOnly? Year { get; set; }
    public string? Description { get; set; }
    public string? Type { get; set; }
    public string? Image { get; set; }
}