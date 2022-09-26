using App.Contracts.DAL;
using Base.Contracts.Bll;

namespace App.Contracts.BLL.Services;

public interface IGameService : IEntityService<App.Bll.DTO.Game>, IGameRepositoryCustom<App.Bll.DTO.Game> // add custom stuff
{
    
}