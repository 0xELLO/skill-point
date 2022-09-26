using AutoMapper;
using Base.DAL;

namespace App.BLL.Mapper;

public class UserRoundResultMapper :  BaseMapper<App.Bll.DTO.UserRoundResult,App.DAL.DTO.UserRoundResult>
{
    public UserRoundResultMapper(IMapper mapper) : base(mapper)
    {
    }
}