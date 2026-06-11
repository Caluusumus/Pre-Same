namespace MuseoCivicoAurora.shared;

public class Ticket
{
    public Guid Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string Email { get; set; } = default!;
    public string Type { get; set; } = default!;
    public int Quantity { get; set; }
    public decimal Total { get; set; }
    public DateTime? CreatedAt { get; set; }
    public Guid? ExhibitionId { get; set; }
    public Guid? TourId { get; set; }
}