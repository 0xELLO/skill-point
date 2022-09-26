using App.Contracts.DAL;
using Base.Contracts.Bll;

namespace App.Contracts.BLL.Services;

public interface IGameContentService :  IEntityService<App.Bll.DTO.GameContent>, IGameContentRepositoryCustom<App.Bll.DTO.GameContent>
{
    
}