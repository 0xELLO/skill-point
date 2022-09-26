using App.Contracts.DAL;
using App.DAL.DTO;
using Base.Contracts.Base;
using Base.DAL.EF;

namespace App.DAL.EF.Repositories;

public class GameRoundRepository  : BaseEntityRepository<App.DAL.DTO.GameRound, App.Domain.GameRound, AppDbContext>, IGameRoundRepository
{
    public GameRoundRepository(AppDbContext dbContext, IMapper<GameRound, Domain.GameRound> mapper) : base(dbContext, mapper)
    {
    }
}