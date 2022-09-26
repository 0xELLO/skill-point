using Base.Domain;

namespace App.Public.DTO;

public class GameRoundDTO : DomainEntityId
{
    // FK Game
    public Guid GameId { get; set; }
    
    // FK Match
    public Guid MatchId { get; set; }
    
    // FK GameContent
    public Guid? GameContentId { get; set; }
    
    public bool Opened { get; set; } = true;
}