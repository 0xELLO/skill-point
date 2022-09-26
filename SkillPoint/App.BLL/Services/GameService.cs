using App.Bll.DTO;
using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using Base.Bll;
using Base.Contracts.Base;

namespace App.BLL.Services;

public class GameService :  BaseEntityService<App.Bll.DTO.Game, App.DAL.DTO.Game, IGameRepository>,
    IGameService
{
    public GameService(IGameRepository repository, IMapper<App.Bll.DTO.Game, DAL.DTO.Game> mapper) : base(repository, mapper)
    {
    }

    public async Task<IEnumerable<Game>> GetAllByNameAsync(string name, bool noTracking = true)
    {
        return (await Repository.GetAllByNameAsync(name, noTracking)).Select(x => Mapper.Map(x))!;
    }
}