using AutoMapper;
using Base.DAL;

namespace App.BLL.Mapper;

public class GameRoundMapper :  BaseMapper<App.Bll.DTO.GameRound,App.DAL.DTO.GameRound>
{
    public GameRoundMapper(IMapper mapper) : base(mapper)
    {
    }
}