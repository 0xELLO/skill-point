using System.Collections;
using App.Contracts.DAL;
using App.DAL.EF.Mappers;
using App.DAL.EF.Repositories;
using Base.Contracts.DAL;
using Base.DAL.EF;

namespace App.DAL.EF;

public class AppUOW : BaseUOW<AppDbContext>, IAppUnitOfWork
{
    private readonly AutoMapper.IMapper _mapper;

    public AppUOW(AppDbContext dbContext, AutoMapper.IMapper mapper) : base(dbContext)
    {
        _mapper = mapper;
    }
    private IGameCategoryRepository? _gameCategory;
    public virtual IGameCategoryRepository GameCategory => _gameCategory ??= new GameCategoryRepository(UOWDbContext, new GameCategoryMapper(_mapper));
    
    private IGameRepository? _games;
    public virtual IGameRepository Games => _games ??= new GameRepository(UOWDbContext, new GameMapper(_mapper));
    
    private IGameContentRepository? _gameContent;
    public virtual IGameContentRepository GameContent => _gameContent ??= new GameContentRepository(UOWDbContext, new GameContentMapper(_mapper));
    
    private IMatchRepository? _match;
    public virtual IMatchRepository Match => _match ??= new MatchRepository(UOWDbContext, new MatchMapper(_mapper));
    
    private IMatchTypeRepository? _matchType;
    public virtual IMatchTypeRepository MatchType => _matchType ??= new MatchTypeRepository(UOWDbContext, new MatchTypeMapper(_mapper));
    
    private IGameInMatchRepository? _gameInMatch;
    public virtual IGameInMatchRepository GameInMatch => _gameInMatch ??= new GameInMatchRepository(UOWDbContext, new GameInMatchMapper(_mapper));
    
    private IGameRoundRepository? _gameRound;
    public virtual IGameRoundRepository GameRound => _gameRound ??= new GameRoundRepository(UOWDbContext, new GameRoundMapper(_mapper));
    
    private IUserPlayingGameRoundRepository? _userPlayingGameRound;
    public virtual IUserPlayingGameRoundRepository UserPlayingGameRound => _userPlayingGameRound ??= new UserPlayingGameRoundRepository(UOWDbContext, new UserPlayingGameRoundMapper(_mapper));
    
    private IUserRoundResultRepository? _userRoundResult;
    public virtual IUserRoundResultRepository UserRoundResult => _userRoundResult ??= new UserRoundResultRepository(UOWDbContext, new UserRoundResultMapper(_mapper));
    
    private IUserGameStatisticsRepository? _userGameStatistics;
    public virtual IUserGameStatisticsRepository UserGameStatistics => _userGameStatistics ??= new UserGameStatisticsRepository(UOWDbContext, new UserGameStatisticsMapper(_mapper));
    
    private IGameOverallStatisticsRepository? _gameOverallStatistics;
    public virtual IGameOverallStatisticsRepository GameOverallStatistics => _gameOverallStatistics ??= new GameOverallStatisticsRepository(UOWDbContext, new GameOverallStatisticsMapper(_mapper));
    
    private IUserInMatchRepository? _userInMatch;
    public virtual IUserInMatchRepository UserInMatch=> _userInMatch ??= new UserInMatchRepository(UOWDbContext, new UserInMatchMapper(_mapper));
    
    
}