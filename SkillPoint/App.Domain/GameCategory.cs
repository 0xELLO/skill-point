using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Base.Domain;

namespace App.Domain;

public class GameCategory : DomainEntityMetaId
{
    [MaxLength(32)]
    [Column(TypeName = "jsonb")]
    public LangStr Name { get; set; } =  new();
    
    [MaxLength(32)]
    [Column(TypeName = "jsonb")]
    public LangStr Description { get; set; } =  new();

    public ICollection<Game>? Games { get; set; }
}