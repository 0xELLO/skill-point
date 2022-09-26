using AutoMapper;
using Base.DAL;

namespace App.DAL.EF.Mappers;

public class UserPlayingGameRoundMapper : BaseMapper<App.DAL.DTO.UserPlayingGameRound,App.Domain.UserPlayingGameRound>
{
    public UserPlayingGameRoundMapper(IMapper mapper) : base(mapper)
    {
    }
}