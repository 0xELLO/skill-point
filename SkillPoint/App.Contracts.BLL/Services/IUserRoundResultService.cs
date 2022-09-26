using App.Contracts.DAL;
using Base.Contracts.Bll;

namespace App.Contracts.BLL.Services;

public interface IUserRoundResultService :  IEntityService<App.Bll.DTO.UserRoundResult>, IUserRoundResultRepositoryCustom<App.Bll.DTO.UserRoundResult>
{
    
}