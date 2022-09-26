using App.Contracts.DAL;
using Base.Contracts.Bll;

namespace App.Contracts.BLL.Services;

public interface IMatchTypeService : IEntityService<App.Bll.DTO.MatchType>, IMatchTypeRepositoryCustom<App.Bll.DTO.MatchType>
{
    
}