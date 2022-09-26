using App.DAL.DTO;
using Base.Contracts.DAL;

namespace App.Contracts.DAL;

public interface IGameRoundRepository  : IEntityRepository<GameRound>, IGameRoundRepositoryCustom<GameRound>
{
    
}

public interface IGameRoundRepositoryCustom<TEntity>
{
    
}