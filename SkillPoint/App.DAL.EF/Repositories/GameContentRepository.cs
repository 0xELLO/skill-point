using App.Contracts.DAL;
using App.DAL.DTO;
using Base.Contracts.Base;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;
namespace App.DAL.EF.Repositories;

public class GameContentRepository : BaseEntityRepository<GameContent, App.Domain.GameContent, AppDbContext>, IGameContentRepository
{
    public GameContentRepository(AppDbContext dbContext, IMapper<GameContent, Domain.GameContent> mapper) : base(dbContext, mapper)
    {
    }

    public async Task<GameContent> GetRandomTypingGame(bool noTracking = true)
    {
        var query = CreateQuery(noTracking);
        var gamesContents = await query.Where(a => a.Game!.LogoUrl == "keyboard").ToListAsync();
        var rnd = new Random().Next(0, gamesContents.Count);
        return _mapper.Map(gamesContents[rnd])!;
    }
}