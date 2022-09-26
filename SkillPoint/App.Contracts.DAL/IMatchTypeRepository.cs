using App.DAL.DTO;
using Base.Contracts.DAL;

namespace App.Contracts.DAL;

public interface IMatchTypeRepository : IEntityRepository<App.DAL.DTO.MatchType>, IMatchTypeRepositoryCustom<App.DAL.DTO.MatchType>
{
    
}

public interface IMatchTypeRepositoryCustom<TEntity>
{
    Task<TEntity?> GetByName(string name, bool noTracking = true);

}