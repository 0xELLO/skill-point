using App.DAL.DTO;
using Base.Contracts.DAL;

namespace App.Contracts.DAL;

public interface IGameContentRepository : IEntityRepository<GameContent>, IGameContentRepositoryCustom<GameContent>
{
    
}

public interface IGameContentRepositoryCustom<TEntity>
{
    Task<TEntity> GetRandomTypingGame(bool noTracking = true);
}