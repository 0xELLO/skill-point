using AutoMapper;
using Base.DAL;

namespace App.BLL.Mapper;

public class GameOverallStatisticsMapper :  BaseMapper<App.Bll.DTO.GameOverallStatistics,App.DAL.DTO.GameOverallStatistics>
{
    public GameOverallStatisticsMapper(IMapper mapper) : base(mapper)
    {
    }
}