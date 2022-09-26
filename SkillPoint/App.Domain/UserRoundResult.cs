using App.Domain.Identity;
using Base.Domain;

namespace App.Domain;

public class UserRoundResult : DomainEntityMetaId
{
    // FK AppUser
    public Guid AppUserId { get; set; }
    public AppUser? AppUser { get; set; }
    
    // FK GameRound
    public Guid GameRoundId { get; set; }
    public GameRound? GameRound { get; set; }

    public string Result { get; set; } = default!;
}