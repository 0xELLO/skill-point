using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Base.Domain;

namespace App.Domain;

public class MatchType : DomainEntityMetaId
{
    [MaxLength(32)]
    public string Name { get; set; } = default!;

    public ICollection<Match>? Matches { get; set; }
}