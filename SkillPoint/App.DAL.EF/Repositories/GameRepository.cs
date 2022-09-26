using App.Contracts.DAL;
using App.DAL.DTO;
using Base.Contracts.Base;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class GameRepository : BaseEntityRepository<Game, App.Domain.Game, AppDbContext>, IGameRepository
{
    public GameRepository(AppDbContext dbContext, IMapper<Game, App.Domain.Game> mapper) : base(dbContext, mapper)
    {
    }

    public async Task<IEnumerable<Game>> GetAllByNameAsync(string name, bool noTracking = true)
    {
        var query = CreateQuery(noTracking);
        return (await query.Where(a => a.Title.Select(b => b.Value.ToUpper())
            .Contains(name.ToUpper()))
            .ToListAsync()).Select(x => _mapper.Map(x)!);
    }
    
    /*
    public override Task<IEnumerable<Game>> GetAllAsync(bool noTracking = true)
    {
        var query = CreateQuery(noTracking);
        query = query.Include(u => u.AppUser);
        return await query.ToListAsync();
    }
    */
}