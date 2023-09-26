using System.Linq.Expressions;

namespace Repository.Contracts
{
    public interface IRepositoryBase<T>
    {
        IQueryable<T> FindAll(bool trackChanges);
        IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges);
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
        void MultipleDelete(IEnumerable<T> entities);
        void MultipleCreate(IEnumerable<T> entities);
        void MultipleUpdate(IEnumerable<T> entities);
    }
}
