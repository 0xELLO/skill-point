using AutoMapper;
using Base.DAL;

namespace App.BLL.Mapper;

public class UserInMatchMapper :  BaseMapper<App.Bll.DTO.UserInMatch,App.DAL.DTO.UserInMatch>
{
    public UserInMatchMapper(IMapper mapper) : base(mapper)
    {
    }
}