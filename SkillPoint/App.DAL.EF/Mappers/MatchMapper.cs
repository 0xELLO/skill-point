using AutoMapper;
using Base.DAL;

namespace App.DAL.EF.Mappers;

public class MatchMapper : BaseMapper<App.DAL.DTO.Match,App.Domain.Match>
{
    public MatchMapper(IMapper mapper) : base(mapper)
    {
    }
}