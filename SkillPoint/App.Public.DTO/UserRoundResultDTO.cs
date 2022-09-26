using Base.Domain;

namespace App.Public.DTO;

public class UserRoundResultDTO : DomainEntityId
{
    public Guid? AppUserId { get; set; }
    
    // FK GameRound
    public Guid GameRoundId { get; set; }

    public string Result { get; set; } = default!;
    
    public string? Email { get; set; } = default!;
}