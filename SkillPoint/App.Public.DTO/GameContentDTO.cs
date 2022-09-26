using Base.Domain;

namespace App.Public.DTO;

public class GameContentDTO : DomainEntityId
{
    public Guid GameId { get; set; }
    public string Content { get; set; } = default!;
}