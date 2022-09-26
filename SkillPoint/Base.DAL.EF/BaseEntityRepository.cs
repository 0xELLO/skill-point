using Base.Contracts.Base;
using Base.Contracts.DAL;
using Base.Contracts.Domain;
using Microsoft.EntityFrameworkCore;

namespace Base.DAL.EF;
// TODO: do not fetch unneeded data from DB on every request
public class BaseEntityRepository<TDalEntity, TDomainEntity, TDbContext>
    : BaseEntityRepository<TDalEntity, TDomainEntity, Guid, TDbContext>
    where TDalEntity : class, IDomainEntityId<Guid>
    where TDomainEntity : class, IDomainEntityId<Guid>
    where TDbContext : DbContext
{
    public BaseEntityRepository(TDbContext dbContext, IMapper<TDalEntity,TDomainEntity> mapper) : base(dbContext, mapper)
    {
    }
}

public class BaseEntityRepository<TDalEntity, TDomainEntity, TKey, TDbContext>
    : IEntityRepository<TDalEntity, TKey>
    where TDalEntity : class, IDomainEntityId<TKey>
    where TDomainEntity : class, IDomainEntityId<TKey>
    where TKey : IEquatable<TKey>
    where TDbContext : DbContext
{
    protected readonly TDbContext RedoDbContext;
    protected readonly DbSet<TDomainEntity> RepoDbSet;
    protected readonly IMapper<TDalEntity, TDomainEntity> _mapper;

    public BaseEntityRepository(TDbContext dbContext, IMapper<TDalEntity,TDomainEntity> mapper)
    {
        RedoDbContext = dbContext;
        RepoDbSet = dbContext.Set<TDomainEntity>();
        _mapper = mapper;
    }

    protected virtual IQueryable<TDomainEntity> CreateQuery(bool noTracking = true)
    {
        // TODO: entity ownership control
        
        var query = RepoDbSet.AsQueryable();
        if (noTracking)
        {
            query = query.AsNoTracking();
        }
        return query;
    }
    
    public virtual TDalEntity Add(TDalEntity entity)
    {
        return _mapper.Map(RepoDbSet.Add(_mapper.Map(entity)!).Entity)!;
    }

    public virtual TDalEntity Update(TDalEntity entity)
    {
        return _mapper.Map(RepoDbSet.Update(_mapper.Map(entity)!).Entity)!;
    }

    public virtual TDalEntity Remove(TDalEntity entity)
    {
        return _mapper.Map(RepoDbSet.Remove(_mapper.Map(entity)!).Entity)!;
    }

    public virtual TDalEntity Remove(TKey id)
    {
        var entity = FirstOrDefault(id);
        if (entity == null)
        {
            // TODO: implement custom exception for entity noy found
            throw new NullReferenceException($"Entity {typeof(TDalEntity).Name} with id {id} does not exist");
        }
        return Remove(entity);
    }

    public virtual TDalEntity? FirstOrDefault(TKey id, bool noTracking = true)
    {
        return _mapper
            .Map(
                CreateQuery(noTracking)
                    .FirstOrDefault(a => a.Id.Equals(id)));
    }

    public virtual IEnumerable<TDalEntity> GetAll(bool noTracking = true)
    {
        return CreateQuery(noTracking).ToList().Select(x => _mapper.Map(x)!);
    }

    public virtual bool Exists(TKey id)
    {
        return RepoDbSet.Any(a => a.Id.Equals(id));
    }

    public virtual async Task<TDalEntity?> FirstOrDefaultAsync(TKey id, bool noTracking = true)
    {
        return _mapper.Map(await CreateQuery(noTracking).FirstOrDefaultAsync(a => a.Id.Equals(id)));
    }

    public virtual async Task<IEnumerable<TDalEntity>> GetAllAsync(bool noTracking = true)
    {
        return (await CreateQuery(noTracking).ToListAsync()).Select(x => _mapper.Map(x)!);
    }

    public virtual async Task<bool> ExistsAsync(TKey id)
    {
        return await RepoDbSet.AnyAsync(a => a.Id.Equals(id));
    }

    public virtual async Task<TDalEntity> RemoveAsync(TKey id)
    {
        var entity = await FirstOrDefaultAsync(id);
        if (entity == null)
        {
            // TODO: implement custom exception for entity noy found
            throw new NullReferenceException($"Entity {typeof(TDalEntity).Name} with id {id} does not exist");
        }
        return Remove(entity);
    }
}