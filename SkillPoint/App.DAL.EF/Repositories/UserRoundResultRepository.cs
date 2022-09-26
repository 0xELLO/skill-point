using App.Contracts.DAL;
using App.DAL.DTO;
using Base.Contracts.Base;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class UserRoundResultRepository : BaseEntityRepository<App.DAL.DTO.UserRoundResult, App.Domain.UserRoundResult, AppDbContext>, IUserRoundResultRepository
{
    public UserRoundResultRepository(AppDbContext dbContext, IMapper<UserRoundResult, Domain.UserRoundResult> mapper) : base(dbContext, mapper)
    {
    }

    public async Task<IEnumerable<UserRoundResult>> GetAllByUserId(Guid userId, bool noTracking = true)
    {
        var query = CreateQuery(noTracking);
        return (await query.Where(a => a.AppUserId == userId)
            .ToListAsync()).Select(x => _mapper.Map(x)!);
    }
}