using App.BLL.Mapper;
using App.BLL.Services;
using App.Contracts.BLL;
using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using AutoMapper;
using Base.Bll;

namespace App.BLL;
public class AppBLL : BaseBll<IAppUnitOfWork>, IAppBll
{
    protected IAppUnitOfWork UnitOfWork;
    private readonly AutoMapper.IMapper _mapper;
    
    public AppBLL(IAppUnitOfWork unitOfWork, IMapper mapper)
    {
        UnitOfWork = unitOfWork;
        _mapper = mapper;
    }
    
    public override async Task<int> SaveChangesAsync()
    {
        return await UnitOfWork.SaveChangesAsync();
    }

    public override int SaveChanges()
    {
        return UnitOfWork.SaveChanges();
    }

    private IGameService? _games;
    public IGameService Games => _games ??= new GameService(UnitOfWork.Games, new GameMapper(_mapper));

    private IGameCategoryService? _gameCategories;
    public IGameCategoryService GameCategoryServices => _gameCategories ??= new GameCategoryService(UnitOfWork.GameCategory, new GameCategoryMapper(_mapper));
    
    private IGameContentService? _gameContent;
    public IGameContentService GameContentServices => _gameContent ??= new GameContentService(UnitOfWork.GameContent, new GameContentMapper(_mapper));
    
    private IMatchService? _matchService;
    public IMatchService MatchService => _matchService ??= new MatchService(UnitOfWork.Match, new MatchMapper(_mapper));
    
    private IMatchTypeService? _matchTypeService;
    public IMatchTypeService MatchTypeService => _matchTypeService ??= new MatchTypeService(UnitOfWork.MatchType, new MatchTypeMapper(_mapper));
    
    private IGameInMatchService? _gameInMatchService;
    public IGameInMatchService GameInMatchService => _gameInMatchService ??= new GameInMatchService(UnitOfWork.GameInMatch, new GameInMatchMapper(_mapper));

    private IGameRoundService? _gameRoundService;
    public IGameRoundService GameRoundService => _gameRoundService ??= new GameRoundService(UnitOfWork.GameRound, new GameRoundMapper(_mapper));
    
    private IUserPlayingGameRoundService? _userPlayingGameRoundService;
    public IUserPlayingGameRoundService UserPlayingGameRoundService => _userPlayingGameRoundService ??= new UserPlayingGameRoundService(UnitOfWork.UserPlayingGameRound, new UserPlayingGameRoundMapper(_mapper));
    
    private IUserRoundResultService? _userRoundResultService;
    public IUserRoundResultService UserRoundResultService => _userRoundResultService ??= new UserRoundResultService(UnitOfWork.UserRoundResult, new UserRoundResultMapper(_mapper));
    
    private IUserGameStatisticsService? _userGameStatistics;
    public IUserGameStatisticsService UserGameStatisticsService => _userGameStatistics ??= new UserGameStatisticsService(UnitOfWork.UserGameStatistics, new UserGameStatisticsMapper(_mapper));
    
    private IGameOverallStatisticsService? _gameOverallStatisticsService;
    public IGameOverallStatisticsService GameOverallStatisticsService => _gameOverallStatisticsService ??= new GameOverallStatisticsService(UnitOfWork.GameOverallStatistics, new GameOverallStatisticsMapper(_mapper));
    
    private IUserInMatchService? _userInMatchService;
    public IUserInMatchService UserInMatchService => _userInMatchService ??= new UserInMatchService(UnitOfWork.UserInMatch, new UserInMatchMapper(_mapper));
}

  