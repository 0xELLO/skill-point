using App.Contracts.DAL;
using App.DAL.DTO;
using Base.Contracts.Base;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class UserGameStatisticsRepository : BaseEntityRepository<App.DAL.DTO.UserGameStatistics, App.Domain.UserGameStatistics, AppDbContext>, IUserGameStatisticsRepository
{
    public UserGameStatisticsRepository(AppDbContext dbContext, IMapper<UserGameStatistics, Domain.UserGameStatistics> mapper) : base(dbContext, mapper)
    {
    }

    public async Task<IEnumerable<UserGameStatistics?>> GetByUserId(Guid userId, bool noTracking = true)
    {
        var query = CreateQuery(noTracking);
        return (await query.Where(a => a.AppUserId == userId).ToListAsync()).Select(x => _mapper.Map(x)!);
    }
}