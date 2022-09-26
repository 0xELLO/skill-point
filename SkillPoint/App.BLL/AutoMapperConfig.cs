using App.DAL.DTO.Identity;
using AutoMapper;

namespace App.BLL;

public class AutoMapperConfig : Profile
{
    public AutoMapperConfig()
    {
        
        CreateMap<App.Bll.DTO.Game, App.DAL.DTO.Game>().ReverseMap();
        CreateMap<App.Bll.DTO.GameCategory, App.DAL.DTO.GameCategory>().ReverseMap();
        CreateMap<App.Bll.DTO.GameContent, App.DAL.DTO.GameContent>().ReverseMap();
        CreateMap<App.Bll.DTO.Match, App.DAL.DTO.Match>().ReverseMap();
        CreateMap<App.Bll.DTO.MatchType, App.DAL.DTO.MatchType>().ReverseMap();
        CreateMap<App.Bll.DTO.GameInMatch, App.DAL.DTO.GameInMatch>().ReverseMap();
        CreateMap<App.Bll.DTO.GameRound, App.DAL.DTO.GameRound>().ReverseMap();
        CreateMap<App.Bll.DTO.UserPlayingGameRound, App.DAL.DTO.UserPlayingGameRound>().ReverseMap();
        CreateMap<App.Bll.DTO.UserRoundResult, App.DAL.DTO.UserRoundResult>().ReverseMap();
        CreateMap<App.Bll.DTO.UserGameStatistics, App.DAL.DTO.UserGameStatistics>().ReverseMap();
        CreateMap<App.Bll.DTO.GameOverallStatistics, App.DAL.DTO.GameOverallStatistics>().ReverseMap();
        CreateMap<App.Bll.DTO.UserInMatch, App.DAL.DTO.UserInMatch>().ReverseMap();
        CreateMap<App.Bll.DTO.Identity.AppUser, App.DAL.DTO.Identity.AppUser>().ReverseMap();
        
    }
}