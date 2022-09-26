using App.Bll.DTO;
using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using Base.Bll;
using Base.Contracts.Base;

namespace App.BLL.Services;

public class GameContentService : BaseEntityService<App.Bll.DTO.GameContent, App.DAL.DTO.GameContent, IGameContentRepository>,
    IGameContentService
{
    public GameContentService(IGameContentRepository repository, IMapper<GameContent, DAL.DTO.GameContent> mapper) : base(repository, mapper)
    {
    }

    public async Task<GameContent> GetRandomTypingGame(bool noTracking = true)
    {
        return Mapper.Map(await Repository.GetRandomTypingGame(noTracking))!;
    }
}