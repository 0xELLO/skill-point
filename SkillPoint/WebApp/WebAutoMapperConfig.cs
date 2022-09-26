using AutoMapper;

namespace WebApp;

public class WebAutoMapperConfig : Profile
{
    public WebAutoMapperConfig()
    {
        CreateMap<App.Public.DTO.GameDTO, App.Bll.DTO.Game>().ReverseMap();
    }
}