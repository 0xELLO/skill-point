using App.Bll.DTO;
using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using Base.Bll;
using Base.Contracts.Base;

namespace App.BLL.Services;

public class UserGameStatisticsService : BaseEntityService<App.Bll.DTO.UserGameStatistics, App.DAL.DTO.UserGameStatistics, IUserGameStatisticsRepository>,
    IUserGameStatisticsService
{
    public UserGameStatisticsService(IUserGameStatisticsRepository repository, IMapper<UserGameStatistics, DAL.DTO.UserGameStatistics> mapper) : base(repository, mapper)
    {
    }

    public async Task<IEnumerable<UserGameStatistics?>> GetByUserId(Guid userId, bool noTracking = true)
    {
        return (await Repository.GetByUserId(userId, noTracking)).Select(x => Mapper.Map(x))!;
    }
}