using App.Contracts.DAL;
using Base.Contracts.Bll;

namespace App.Contracts.BLL.Services;

public interface IUserInMatchService :  IEntityService<App.Bll.DTO.UserInMatch>, IUserInMatchRepositoryCustom<App.Bll.DTO.UserInMatch>
{
    
}