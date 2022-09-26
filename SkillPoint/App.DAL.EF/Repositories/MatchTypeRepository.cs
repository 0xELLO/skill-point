using App.Contracts.DAL;
using App.DAL.DTO;
using Base.Contracts.Base;
using Base.DAL.EF;
using MatchType = App.DAL.DTO.MatchType;

namespace App.DAL.EF.Repositories;

public class MatchTypeRepository : BaseEntityRepository<App.DAL.DTO.MatchType, App.Domain.MatchType, AppDbContext>, IMatchTypeRepository
{
    public MatchTypeRepository(AppDbContext dbContext, IMapper<MatchType, Domain.MatchType> mapper) : base(dbContext, mapper)
    {
    }

    public async Task<MatchType?> GetByName(string name, bool noTracking = true)
    {
        var query = CreateQuery(noTracking);
        return _mapper.Map(query.FirstOrDefault(a => a.Name == name));
    }
}