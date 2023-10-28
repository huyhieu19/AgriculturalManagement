using System.Linq.Expressions;

namespace Repository.Contracts
{
    public interface IRepositoryBase<T>
    {

        // Get List methods
        IQueryable<T> FindAll(bool trackChanges);
        IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges);
        // Create methods
        void Create(T entity);
        Task MultipleCreate(IEnumerable<T> entities);
        // Update methods
        void Update(T entity);
        void MultipleUpdate(IEnumerable<T> entities);
        // Delete methods
        void Delete(T entity);
        void MultipleDelete(IEnumerable<T> entities);
    }
}
