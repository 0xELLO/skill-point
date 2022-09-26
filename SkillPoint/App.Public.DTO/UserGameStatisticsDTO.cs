using Base.Domain;

namespace App.Public.DTO;

public class UserGameStatisticsDTO : DomainEntityId
{
    public Guid AppUserId { get; set; }
    
    // FK Game
    public Guid GameId { get; set; }

    public int AverageScore { get; set; } = default!;
    public int BestScore { get; set; } = default!;
    public int Rating { get; set; } = default!;
}