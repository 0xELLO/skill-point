using App.Contracts.BLL.Services;
using Base.Contracts.Bll;

namespace App.Contracts.BLL;

public interface IAppBll : IBll
{
    IGameService Games { get; }
    IGameCategoryService GameCategoryServices { get; }
    IGameContentService GameContentServices { get; }
    IMatchService MatchService { get; }
    IMatchTypeService MatchTypeService { get; }
    IGameInMatchService GameInMatchService { get; }
    IGameRoundService GameRoundService { get; }
    IUserPlayingGameRoundService UserPlayingGameRoundService { get; }
    IUserRoundResultService UserRoundResultService { get; }
    IUserGameStatisticsService UserGameStatisticsService { get; }
    IGameOverallStatisticsService GameOverallStatisticsService { get; }
    IUserInMatchService UserInMatchService { get; }
}