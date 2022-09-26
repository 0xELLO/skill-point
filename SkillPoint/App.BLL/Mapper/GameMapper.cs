using AutoMapper;
using Base.DAL;

namespace App.BLL.Mapper;

public class GameMapper : BaseMapper<App.Bll.DTO.Game,App.DAL.DTO.Game>
{
    public GameMapper(IMapper mapper) : base(mapper)
    {
    }
}