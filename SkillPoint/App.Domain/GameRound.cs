using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace App.Domain;

public class GameRound :  DomainEntityMetaId
{
    // FK Game
    public Guid GameId { get; set; }
    public Game? Game { get; set; }
    
    // FK Match
    public Guid MatchId { get; set; }
    public Match? Match { get; set; }

    public bool Opened { get; set; } = true;
    
    // FK GameContent
    public Guid? GameContentId { get; set; }
    public GameContent? GameContent { get; set; }

    public ICollection<UserPlayingGameRound>? UsersPlayingGameRound { get; set; }
    public ICollection<UserRoundResult>? UserRoundResults { get; set; }
}