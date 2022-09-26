using App.Bll.DTO;
using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using Base.Bll;
using Base.Contracts.Base;

namespace App.BLL.Services;

public class GameCategoryService : BaseEntityService<App.Bll.DTO.GameCategory, App.DAL.DTO.GameCategory, IGameCategoryRepository>,
    IGameCategoryService
{
    public GameCategoryService(IGameCategoryRepository repository, IMapper<App.Bll.DTO.GameCategory, DAL.DTO.GameCategory> mapper) : base(repository, mapper)
    {
    }
}