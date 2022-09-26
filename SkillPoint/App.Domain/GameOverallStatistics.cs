using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace App.Domain;

public class GameOverallStatistics :  DomainEntityMetaId
{
    [MaxLength(4094)]
    // TODO representation as json, similar to LangStr
    public string AverageScoreDistribution { get; set; } = default!;
}