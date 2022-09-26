using App.Contracts.DAL;
using Base.Contracts.Bll;

namespace App.Contracts.BLL.Services;

public interface IMatchService : IEntityService<App.Bll.DTO.Match>, IMatchRepositoryCustom<App.Bll.DTO.Match>
{
    
}