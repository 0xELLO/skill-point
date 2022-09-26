using App.Bll.DTO;
using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using Base.Bll;
using Base.Contracts.Base;

namespace App.BLL.Services;

public class GameRoundService : BaseEntityService<App.Bll.DTO.GameRound, App.DAL.DTO.GameRound, IGameRoundRepository>,
    IGameRoundService
{
    public GameRoundService(IGameRoundRepository repository, IMapper<GameRound, DAL.DTO.GameRound> mapper) : base(repository, mapper)
    {
    }
}