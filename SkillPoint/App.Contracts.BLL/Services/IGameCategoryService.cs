using App.Contracts.DAL;
using Base.Contracts.Bll;

namespace App.Contracts.BLL.Services;

public interface IGameCategoryService :  IEntityService<App.Bll.DTO.GameCategory>, IGameCategoryRepositoryCustom<App.Bll.DTO.GameCategory>
{
    
}