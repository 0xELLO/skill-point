using App.Bll.DTO;
using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using Base.Bll;
using Base.Contracts.Base;

namespace App.BLL.Services;

public class UserRoundResultService : BaseEntityService<App.Bll.DTO.UserRoundResult, App.DAL.DTO.UserRoundResult, IUserRoundResultRepository>,
    IUserRoundResultService
{
    public UserRoundResultService(IUserRoundResultRepository repository, IMapper<UserRoundResult, DAL.DTO.UserRoundResult> mapper) : base(repository, mapper)
    {
    }

    public async Task<IEnumerable<UserRoundResult>> GetAllByUserId(Guid userId, bool noTracking = true)
    {
        return (await Repository.GetAllByUserId(userId, noTracking)).Select(x => Mapper.Map(x))!;
    }
}