using App.Contracts.DAL;
using Base.Contracts.Bll;

namespace App.Contracts.BLL.Services;

public interface IGameOverallStatisticsService :  IEntityService<App.Bll.DTO.GameOverallStatistics>, IGameOverallStatisticsRepositoryCustom<App.Bll.DTO.GameOverallStatistics>
{
    
}