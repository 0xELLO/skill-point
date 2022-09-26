using AutoMapper;
using Base.DAL;

namespace App.DAL.EF.Mappers;

public class GameOverallStatisticsMapper : BaseMapper<App.DAL.DTO.GameOverallStatistics,App.Domain.GameOverallStatistics>
{
    public GameOverallStatisticsMapper(IMapper mapper) : base(mapper)
    {
    }
}