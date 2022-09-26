using App.Bll.DTO;
using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using Base.Bll;
using Base.Contracts.Base;
using MatchType = App.Bll.DTO.MatchType;

namespace App.BLL.Services;

public class MatchTypeService : BaseEntityService<App.Bll.DTO.MatchType, App.DAL.DTO.MatchType, IMatchTypeRepository>,
    IMatchTypeService
{
    public MatchTypeService(IMatchTypeRepository repository, IMapper<MatchType, DAL.DTO.MatchType> mapper) : base(repository, mapper)
    {
    }

    public async Task<MatchType?> GetByName(string name, bool noTracking = true)
    {
        return Mapper.Map(await Repository.GetByName(name, noTracking));
    }
}