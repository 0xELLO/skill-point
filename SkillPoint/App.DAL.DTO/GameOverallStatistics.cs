using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace App.DAL.DTO;

public class GameOverallStatistics :  DomainEntityMetaId
{
    [MaxLength(4094)]
    public string AverageScoreDistribution { get; set; } = default!;
}