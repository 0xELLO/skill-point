using App.Contracts.DAL;
using App.DAL.DTO;
using Base.Contracts.Base;
using Base.DAL.EF;

namespace App.DAL.EF.Repositories;

public class GameOverallStatisticsRepository : BaseEntityRepository<App.DAL.DTO.GameOverallStatistics, App.Domain.GameOverallStatistics, AppDbContext>, IGameOverallStatisticsRepository
{
    public GameOverallStatisticsRepository(AppDbContext dbContext, IMapper<GameOverallStatistics, Domain.GameOverallStatistics> mapper) : base(dbContext, mapper)
    {
    }
}