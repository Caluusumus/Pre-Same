namespace MuseoCivicoAurora.shared;

public class Exhibition
{
    public Guid Id { get; set; }
    public string Title { get; set; } = default!;
    public string? Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string? Image { get; set; }
    public string Status { get; set; } = default!;
    public List<Artwork> Artworks { get; set; } = new List<Artwork>();
}