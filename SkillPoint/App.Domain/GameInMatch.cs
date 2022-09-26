using Base.Domain;

namespace App.Domain;

public class GameInMatch : DomainEntityMetaId
{
    // FK Match
    public Guid MatchId { get; set; }
    public Match? Match { get; set; }

    // FK Game
    public Guid GameId { get; set; }
    public Game? Game { get; set; }

    public int RoundAmount { get; set; } = 1;
}