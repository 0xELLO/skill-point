using App.DAL.DTO;
using Base.Contracts.DAL;

namespace App.Contracts.DAL;

public interface IGameRepository : IEntityRepository<Game>, IGameRepositoryCustom<Game>
{

}

public interface IGameRepositoryCustom<TEntity>
{
    Task<IEnumerable<TEntity>> GetAllByNameAsync(string name, bool noTracking = true);
}