using App.Bll.DTO;
using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using Base.Bll;
using Base.Contracts.Base;

namespace App.BLL.Services;

public class UserInMatchService : BaseEntityService<App.Bll.DTO.UserInMatch, App.DAL.DTO.UserInMatch, IUserInMatchRepository>,
    IUserInMatchService
{
    public UserInMatchService(IUserInMatchRepository repository, IMapper<UserInMatch, DAL.DTO.UserInMatch> mapper) : base(repository, mapper)
    {
    }

    public async Task<IEnumerable<string>> GetJoinedUserEmail(Guid matchId, bool noTracking = true)
    {
        return await Repository.GetJoinedUserEmail(matchId);
    }

    public async Task<bool> UserAlreadyInMatch(Guid id, bool noTracking = true)
    {
        return await Repository.UserAlreadyInMatch(id);
    }
}