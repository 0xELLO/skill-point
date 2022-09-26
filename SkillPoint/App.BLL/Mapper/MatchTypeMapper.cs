using AutoMapper;
using Base.DAL;

namespace App.BLL.Mapper;

public class MatchTypeMapper : BaseMapper<App.Bll.DTO.MatchType,App.DAL.DTO.MatchType>
{
    public MatchTypeMapper(IMapper mapper) : base(mapper)
    {
    }
}