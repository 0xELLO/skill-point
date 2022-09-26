using App.Contracts.DAL;
using App.DAL.DTO;
using Base.Contracts.Base;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class MatchRepository :  BaseEntityRepository<Match, App.Domain.Match, AppDbContext>, IMatchRepository
{
    public MatchRepository(AppDbContext dbContext, IMapper<Match, Domain.Match> mapper) : base(dbContext, mapper)
    {
    }

    public async Task<Match?> GetMatchByToken(string token, bool noTracking = true)
    {
        var query = CreateQuery(noTracking);
        return _mapper.Map(await query.Where(a => a.MatchToken == token).FirstAsync());
    }
}