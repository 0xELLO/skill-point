using App.Contracts.DAL;
using App.DAL.DTO;
using Base.Contracts.Base;
using Base.DAL.EF;

namespace App.DAL.EF.Repositories;

public class GameInMatchRepository : BaseEntityRepository<GameInMatch, App.Domain.GameInMatch, AppDbContext>, IGameInMatchRepository
{
    public GameInMatchRepository(AppDbContext dbContext, IMapper<GameInMatch, Domain.GameInMatch> mapper) : base(dbContext, mapper)
    {
    }
}