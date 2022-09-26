using AutoMapper;
using Base.DAL;

namespace WebApp.Mappers;

public class GameMapper : BaseMapper<App.Public.DTO.GameDTO, App.Bll.DTO.Game>
{
    public GameMapper(IMapper mapper) : base(mapper)
    {
    }
}