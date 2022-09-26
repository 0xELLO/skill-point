using AutoMapper;
using Base.DAL;

namespace App.BLL.Mapper;

public class UserPlayingGameRoundMapper :  BaseMapper<App.Bll.DTO.UserPlayingGameRound,App.DAL.DTO.UserPlayingGameRound>
{
    public UserPlayingGameRoundMapper(IMapper mapper) : base(mapper)
    {
    }
}