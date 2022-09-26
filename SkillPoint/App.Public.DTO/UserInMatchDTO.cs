using Base.Domain;

namespace App.Public.DTO;

public class UserInMatchDTO : DomainEntityId
{
    public Guid AppUserId { get; set; }

    // FK Match
    public Guid MatchId { get; set; }
}