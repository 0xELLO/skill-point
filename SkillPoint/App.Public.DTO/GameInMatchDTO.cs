using Base.Domain;

namespace App.Public.DTO;

public class GameInMatchDTO : DomainEntityId
{
    public Guid MatchId { get; set; }

    // FK Game
    public Guid GameId { get; set; }

    public int RoundAmount { get; set; } = 1;
}