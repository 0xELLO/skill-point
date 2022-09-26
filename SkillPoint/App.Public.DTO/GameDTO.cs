using Base.Domain;

namespace App.Public.DTO;

public class GameDTO : DomainEntityId
{
    public string Title { get; set; } = default!;
    public string ShortDescription { get; set; } = default!;
    public string LongDescription { get; set; } = default!;
    
    public string LogoUrl { get; set; } = default!;
}