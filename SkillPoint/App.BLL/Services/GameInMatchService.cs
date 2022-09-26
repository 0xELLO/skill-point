using App.Bll.DTO;
using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using Base.Bll;
using Base.Contracts.Base;

namespace App.BLL.Services;

public class GameInMatchService : BaseEntityService<App.Bll.DTO.GameInMatch, App.DAL.DTO.GameInMatch, IGameInMatchRepository>,
    IGameInMatchService
{
    public GameInMatchService(IGameInMatchRepository repository, IMapper<GameInMatch, DAL.DTO.GameInMatch> mapper) : base(repository, mapper)
    {
    }
}