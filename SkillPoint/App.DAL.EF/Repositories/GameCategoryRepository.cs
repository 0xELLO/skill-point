using App.Contracts.DAL;
using App.DAL.DTO;
using Base.Contracts.Base;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;


namespace App.DAL.EF.Repositories;

public class GameCategoryRepository: BaseEntityRepository<GameCategory, App.Domain.GameCategory, AppDbContext>, IGameCategoryRepository
{
    public GameCategoryRepository(AppDbContext dbContext, IMapper<GameCategory, App.Domain.GameCategory> mapper) : base(dbContext, mapper)
    {
    }
}