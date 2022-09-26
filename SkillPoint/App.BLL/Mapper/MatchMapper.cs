using AutoMapper;
using Base.DAL;

namespace App.BLL.Mapper;

public class MatchMapper :  BaseMapper<App.Bll.DTO.Match,App.DAL.DTO.Match>
{
    public MatchMapper(IMapper mapper) : base(mapper)
    {
    }
}