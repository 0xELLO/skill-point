using App.Contracts.DAL;
using Base.Contracts.Bll;

namespace App.Contracts.BLL.Services;

public interface IUserPlayingGameRoundService :  IEntityService<App.Bll.DTO.UserPlayingGameRound>, IUserPlayingGameRoundRepositoryCustom<App.Bll.DTO.UserPlayingGameRound>
{
    
}