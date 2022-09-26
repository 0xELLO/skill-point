using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace App.Bll.DTO;

public class GameContent : DomainEntityId
{
    // FK Game
    public Guid GameId { get; set; }
    public Game? Game { get; set; }

    [MaxLength(4094)]
    public string Content { get; set; } = default!;
    
   // public ICollection<GameRound>? GameRounds { get; set; }
}