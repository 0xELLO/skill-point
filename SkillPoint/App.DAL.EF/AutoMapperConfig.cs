using  App.DAL.DTO;
using App.DAL.DTO.Identity;
using AutoMapper;
using MatchType = App.DAL.DTO.MatchType;

namespace App.DAL.EF;

public class AutoMapperConfig: Profile
{
    public AutoMapperConfig()
    {
        CreateMap<Game, App.Domain.Game>().ReverseMap();
        CreateMap<GameCategory, App.Domain.GameCategory>().ReverseMap();
        CreateMap<GameContent, App.Domain.GameContent>().ReverseMap();
        CreateMap<Match, App.Domain.Match>().ReverseMap();
        CreateMap<App.DAL.DTO.MatchType, App.Domain.MatchType>().ReverseMap();
        CreateMap<App.DAL.DTO.GameInMatch, App.Domain.GameInMatch>().ReverseMap();
        CreateMap<App.DAL.DTO.GameRound, App.Domain.GameRound>().ReverseMap();
        CreateMap<App.DAL.DTO.UserPlayingGameRound, App.Domain.UserPlayingGameRound>().ReverseMap();
        CreateMap<App.DAL.DTO.UserGameStatistics, App.Domain.UserGameStatistics>().ReverseMap();
        CreateMap<App.DAL.DTO.GameOverallStatistics, App.Domain.GameOverallStatistics>().ReverseMap();
        CreateMap<App.DAL.DTO.UserRoundResult, App.Domain.UserRoundResult>().ReverseMap();
        CreateMap<App.DAL.DTO.UserInMatch, App.Domain.UserInMatch>().ReverseMap();
        CreateMap<AppUser, App.Domain.Identity.AppUser>().ReverseMap();
    }
}