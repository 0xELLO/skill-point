using App.DAL.DTO.Identity;
using AutoMapper;
using Base.DAL;

namespace App.BLL.Mapper.Identity;

public class AppUserMapper : BaseMapper<App.Bll.DTO.Identity.AppUser, App.DAL.DTO.Identity.AppUser>
{
    public AppUserMapper(IMapper mapper) : base(mapper)
    {
    }
}