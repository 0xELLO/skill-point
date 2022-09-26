using Base.Contracts.Bll;
using Base.Contracts.DAL;

namespace Base.Bll;

public abstract class BaseBll<TDal> : IBll
where TDal: IUnitOfWork
{
    public abstract Task<int> SaveChangesAsync();
    public abstract int SaveChanges();
}