using Infrastructure.Repositories.Interfaces;

namespace Utils.UnitOfWork.Interfaces
{
    public interface IUnitOfWork
    {
        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : class;

        public Task<int> SaveChangesAsync();

        public void Dispose();
    }
}
