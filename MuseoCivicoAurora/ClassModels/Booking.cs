namespace ClassModels;

public class Booking
{
    public Guid Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string Email { get; set; } = default!;
    public int ParticipantsCount { get; set; }
    public Guid TourId { get; set; }
    public DateTime? CreatedAt { get; set; }
    public string Status { get; set; } = default!;
}