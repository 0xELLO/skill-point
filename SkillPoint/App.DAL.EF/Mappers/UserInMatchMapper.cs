using AutoMapper;
using Base.DAL;

namespace App.DAL.EF.Mappers;

public class UserInMatchMapper : BaseMapper<App.DAL.DTO.UserInMatch,App.Domain.UserInMatch>
{
    public UserInMatchMapper(IMapper mapper) : base(mapper)
    {
    }
}