using App.DAL.DTO;
using Base.Contracts.DAL;

namespace App.Contracts.DAL;

public interface IUserGameStatisticsRepository : IEntityRepository<UserGameStatistics>, IUserGameStatisticsRepositoryCustom<UserGameStatistics>
{
    
}

public interface IUserGameStatisticsRepositoryCustom<TEntity>
{
    Task<IEnumerable<TEntity?>> GetByUserId(Guid userId, bool noTracking = true);

}