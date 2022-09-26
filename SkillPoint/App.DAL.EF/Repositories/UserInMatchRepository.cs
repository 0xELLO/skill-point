using App.Contracts.DAL;
using App.DAL.DTO;
using Base.Contracts.Base;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class UserInMatchRepository : BaseEntityRepository<App.DAL.DTO.UserInMatch, App.Domain.UserInMatch, AppDbContext>, IUserInMatchRepository
{
    public UserInMatchRepository(AppDbContext dbContext, IMapper<UserInMatch, Domain.UserInMatch> mapper) : base(dbContext, mapper)
    {
    }

    public async Task<IEnumerable<string>> GetJoinedUserEmail(Guid matchId, bool noTracking = true)
    {
        var query = CreateQuery(noTracking);
        return await query.Where(x => x.MatchId == matchId).Select(x => x.AppUser!.Email).ToListAsync();
    }

    public async Task<bool> UserAlreadyInMatch(Guid id, bool noTracking = true)
    {
        var query = CreateQuery(noTracking);
        return await query.AnyAsync(x => x.AppUserId == id);
    }
}