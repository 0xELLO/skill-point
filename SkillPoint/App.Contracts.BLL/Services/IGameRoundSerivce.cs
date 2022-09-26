using App.Contracts.DAL;
using Base.Contracts.Bll;

namespace App.Contracts.BLL.Services;

public interface IGameRoundService : IEntityService<App.Bll.DTO.GameRound>, IGameRoundRepositoryCustom<App.Bll.DTO.GameRound>
{
    
}