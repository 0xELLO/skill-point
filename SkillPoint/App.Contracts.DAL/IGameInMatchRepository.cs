using App.DAL.DTO;
using Base.Contracts.DAL;

namespace App.Contracts.DAL;

public interface IGameInMatchRepository : IEntityRepository<GameInMatch>, IGameInMatchRepositoryCustom<GameInMatch>
{
    
}

public interface IGameInMatchRepositoryCustom<TEntity>
{
    
}