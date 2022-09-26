using Base.Domain;

namespace App.Public.DTO;

public class MatchDTO : DomainEntityId
{
    public Guid MatchTypeId { get; set; }
    
    public string? MatchToken { get; set; } = default!;
    
    public DateTime? StartTime { get; set; }
    public DateTime? FinishTime { get; set; }
    
    public string? MatchType { get; set; } = default!;
    
    public int MaxPlayers { get; set; } = 5;
    public bool OpenedToJoin { get; set; } = true;
}