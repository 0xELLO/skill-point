using App.DAL.DTO;
using Base.Contracts.DAL;

namespace App.Contracts.DAL;

public interface IUserInMatchRepository : IEntityRepository<UserInMatch>, IUserInMatchRepositoryCustom<UserInMatch>
{
    
}

public interface IUserInMatchRepositoryCustom<TEntity>
{
    Task<IEnumerable<string>> GetJoinedUserEmail(Guid matchId, bool noTracking = true);
    Task<bool> UserAlreadyInMatch(Guid id, bool noTracking = true);
}