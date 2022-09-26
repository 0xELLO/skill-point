using App.DAL.DTO;
using Base.Contracts.DAL;

namespace App.Contracts.DAL;

public interface IUserPlayingGameRoundRepository : IEntityRepository<UserPlayingGameRound>, IUserPlayingGameRoundRepositoryCustom<UserPlayingGameRound>
{
    
}


public interface IUserPlayingGameRoundRepositoryCustom<TEntity>
{
    
}
