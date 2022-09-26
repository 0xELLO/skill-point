using App.Bll.DTO;
using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using Base.Bll;
using Base.Contracts.Base;

namespace App.BLL.Services;

public class UserPlayingGameRoundService : BaseEntityService<App.Bll.DTO.UserPlayingGameRound, App.DAL.DTO.UserPlayingGameRound, IUserPlayingGameRoundRepository>,
    IUserPlayingGameRoundService
{
    public UserPlayingGameRoundService(IUserPlayingGameRoundRepository repository, IMapper<UserPlayingGameRound, DAL.DTO.UserPlayingGameRound> mapper) : base(repository, mapper)
    {
    }
}