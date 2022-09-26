using AutoMapper;
using Base.DAL;

namespace App.DAL.EF.Mappers;

public class GameRoundMapper : BaseMapper<App.DAL.DTO.GameRound,App.Domain.GameRound>
{
    public GameRoundMapper(IMapper mapper) : base(mapper)
    {
    }
}