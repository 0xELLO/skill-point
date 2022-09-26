using App.DAL.DTO;
using Base.Contracts.DAL;

namespace App.Contracts.DAL;

public interface IGameOverallStatisticsRepository : IEntityRepository<GameOverallStatistics>, IGameOverallStatisticsRepositoryCustom<GameOverallStatistics>
{
    
}

public interface IGameOverallStatisticsRepositoryCustom<TEntity>
{
    
}