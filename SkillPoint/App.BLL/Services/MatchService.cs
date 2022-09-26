using App.Bll.DTO;
using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using Base.Bll;
using Base.Contracts.Base;

namespace App.BLL.Services;

public class MatchService : BaseEntityService<App.Bll.DTO.Match, App.DAL.DTO.Match, IMatchRepository>,
    IMatchService
{
    public MatchService(IMatchRepository repository, IMapper<Match, DAL.DTO.Match> mapper) : base(repository, mapper)
    {
    }

    public async Task<Match?> GetMatchByToken(string token, bool noTracking = true)
    {
        return Mapper.Map(await Repository.GetMatchByToken(token, noTracking));
    }
}