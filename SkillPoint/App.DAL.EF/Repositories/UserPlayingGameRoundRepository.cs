using App.Contracts.DAL;
using App.DAL.DTO;
using Base.Contracts.Base;
using Base.DAL.EF;

namespace App.DAL.EF.Repositories;

public class UserPlayingGameRoundRepository : BaseEntityRepository<App.DAL.DTO.UserPlayingGameRound, App.Domain.UserPlayingGameRound, AppDbContext>, IUserPlayingGameRoundRepository
{
    public UserPlayingGameRoundRepository(AppDbContext dbContext, IMapper<UserPlayingGameRound, Domain.UserPlayingGameRound> mapper) : base(dbContext, mapper)
    {
    }
}