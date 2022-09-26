using App.Contracts.DAL;
using Base.Contracts.Bll;

namespace App.Contracts.BLL.Services;

public interface IUserGameStatisticsService :  IEntityService<App.Bll.DTO.UserGameStatistics>, IUserGameStatisticsRepositoryCustom<App.Bll.DTO.UserGameStatistics>
{
    
}