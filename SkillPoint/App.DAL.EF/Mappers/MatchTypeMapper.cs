using AutoMapper;
using Base.DAL;

namespace App.DAL.EF.Mappers;

public class MatchTypeMapper : BaseMapper<App.DAL.DTO.MatchType,App.Domain.MatchType>
{
    public MatchTypeMapper(IMapper mapper) : base(mapper)
    {
    }
}