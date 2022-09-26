using App.DAL.DTO;
using Base.Contracts.DAL;

namespace App.Contracts.DAL;

public interface IUserRoundResultRepository  : IEntityRepository<UserRoundResult>, IUserRoundResultRepositoryCustom<UserRoundResult>
{
    
}

public interface IUserRoundResultRepositoryCustom<TEntity>
{
    Task<IEnumerable<TEntity>> GetAllByUserId(Guid userId, bool noTracking = true);
}