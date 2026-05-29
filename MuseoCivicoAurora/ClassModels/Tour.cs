using System.Data;

namespace ClassModels;

public class Tour
{
    public Guid Id { get; set; }
    public string Title { get; set; } = default!;
    public string? Description { get; set; }
    public DateTime? DateTime { get; set; }
    public TimeOnly? Duration { get; set; }
    public string? Guide { get; set; }
    public int? ParticipantsCount { get; set; }
    public Guid? ExhibitionId { get; set; }
}