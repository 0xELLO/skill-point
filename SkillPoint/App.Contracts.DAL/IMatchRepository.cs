using App.DAL.DTO;
using Base.Contracts.DAL;

namespace App.Contracts.DAL;

public interface IMatchRepository : IEntityRepository<Match>, IMatchRepositoryCustom<Match>
{
    
}

public interface IMatchRepositoryCustom<TEntity>
{
    Task<TEntity?> GetMatchByToken(string token, bool noTracking = true);
}
