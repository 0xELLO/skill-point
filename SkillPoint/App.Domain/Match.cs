using System.ComponentModel.DataAnnotations;
using App.Domain.Chat;
using Base.Contracts.Domain;
using Base.Domain;

namespace App.Domain;

public class Match : DomainEntityMetaId
{
    // FK MatchType
    public Guid MatchTypeId { get; set; }
    public MatchType? MatchType { get; set; }
    
    [MaxLength(64)]
    public string MatchToken { get; set; } = default!;
    
    public DateTime StartTime { get; set; }
    public DateTime? FinishTime { get; set; }
    
    [MaxLength(5)]
    public int MaxPlayers { get; set; } = 5;
    
    public bool OpenedToJoin { get; set; } = true;

    public ICollection<UserInMatch>? UsersInMatch { get; set; }
    
    public ICollection<GameInMatch>? GameInMatches { get; set; }

    public ICollection<GameRound>? GameRounds { get; set; }
    
    // 1 - 1/0
    public ChatRoom? ChatRoom { get; set; }
}