using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace App.Domain;

public class GameContent : DomainEntityMetaId
{
    // FK Game
    public Guid GameId { get; set; }
    public Game? Game { get; set; }

    [MaxLength(4094)]
    // TODO representation as json, similar to LangStr
    public string Content { get; set; } = default!;

    public ICollection<GameRound>? GameRounds { get; set; }
}