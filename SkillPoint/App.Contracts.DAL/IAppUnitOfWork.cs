using Base.Contracts.DAL;

namespace App.Contracts.DAL;

public interface IAppUnitOfWork : IUnitOfWork
{
    IGameRepository Games { get; }
    IGameCategoryRepository GameCategory { get; }
    IGameContentRepository GameContent { get; }
    IMatchRepository Match { get; }
    IMatchTypeRepository MatchType { get; }
    IGameInMatchRepository GameInMatch { get; }
    IGameRoundRepository GameRound { get; }
    IUserPlayingGameRoundRepository UserPlayingGameRound { get; }
    IUserRoundResultRepository UserRoundResult { get; }
    IUserGameStatisticsRepository UserGameStatistics { get; }
    IGameOverallStatisticsRepository GameOverallStatistics { get; }
    IUserInMatchRepository UserInMatch { get; }
}