using App.DAL.DTO;
using Base.Contracts.DAL;

namespace App.Contracts.DAL;

public interface IGameCategoryRepository: IEntityRepository<GameCategory>, IGameCategoryRepositoryCustom<GameCategory>
{
    
}

public interface IGameCategoryRepositoryCustom<TEntity>
{
    
}