using App.Bll.DTO.Identity;
using Base.Domain;

namespace App.Bll.DTO;

public class UserGameStatistics : DomainEntityMetaId
{
    // FK AppUser
    public Guid AppUserId { get; set; }
    public AppUser? AppUser { get; set; }
    
    // FK Game
    public Guid GameId { get; set; }
    public Game? Game { get; set; }

    public int AverageScore { get; set; } = default!;
    public int BestScore { get; set; } = default!;
    public int Rating { get; set; } = default!;
}