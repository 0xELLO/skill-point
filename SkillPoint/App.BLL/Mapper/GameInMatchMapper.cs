using AutoMapper;
using Base.DAL;

namespace App.BLL.Mapper;

public class GameInMatchMapper : BaseMapper<App.Bll.DTO.GameInMatch,App.DAL.DTO.GameInMatch>
{
    public GameInMatchMapper(IMapper mapper) : base(mapper)
    {
    }
}