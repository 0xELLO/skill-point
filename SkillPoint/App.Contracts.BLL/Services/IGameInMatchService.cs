using App.Contracts.DAL;
using Base.Contracts.Bll;

namespace App.Contracts.BLL.Services;

public interface IGameInMatchService : IEntityService<App.Bll.DTO.GameInMatch>, IGameInMatchRepositoryCustom<App.Bll.DTO.GameInMatch>
{
    
}