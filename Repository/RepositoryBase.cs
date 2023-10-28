using Database;
using Microsoft.EntityFrameworkCore;
using Repository.Contracts;
using System.Linq.Expressions;

namespace Repository
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected FactDbContext FactDbContext;

        protected RepositoryBase(FactDbContext factDbContext)
        {
            FactDbContext = factDbContext;
        }
        public void Create(T entity) => FactDbContext.Set<T>().Add(entity);
        public async Task MultipleCreate(IEnumerable<T> entities) => await FactDbContext.Set<T>().AddRangeAsync(entities);

        public void Delete(T entity) => FactDbContext.Set<T>().Remove(entity);
        public void MultipleDelete(IEnumerable<T> entities) => FactDbContext.Set<T>().RemoveRange(entities);


        public IQueryable<T> FindAll(bool trackChanges) => !trackChanges ? FactDbContext.Set<T>().AsNoTracking() : FactDbContext.Set<T>();

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges) => !trackChanges ? FactDbContext.Set<T>().Where(expression).AsNoTracking() : FactDbContext.Set<T>().Where(expression);

        public void Update(T entity) => FactDbContext.Set<T>().Update(entity);

        public void MultipleUpdate(IEnumerable<T> entities)
        {
            FactDbContext.Set<T>().UpdateRange(entities);
        }
    }
}