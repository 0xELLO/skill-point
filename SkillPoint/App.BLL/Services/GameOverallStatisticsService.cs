using App.Bll.DTO;
using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using Base.Bll;
using Base.Contracts.Base;

namespace App.BLL.Services;

public class GameOverallStatisticsService : BaseEntityService<App.Bll.DTO.GameOverallStatistics, App.DAL.DTO.GameOverallStatistics, IGameOverallStatisticsRepository>,
    IGameOverallStatisticsService
{
    public GameOverallStatisticsService(IGameOverallStatisticsRepository repository, IMapper<GameOverallStatistics, DAL.DTO.GameOverallStatistics> mapper) : base(repository, mapper)
    {
    }
}