using System.Linq.Expressions;

namespace JWTAuthenticatonDemo.Application.Contracts.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        IQueryable<TEntity> Queryable { get; }

        TEntity Add(TEntity entity);
        Task<TEntity> AddAsync(TEntity entity);
        List<TEntity> GetAll();
        List<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate);
        Task<List<TEntity>> GetAllAsync();
        Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate);
        TEntity GetById(int id);
        Task<TEntity> GetByIdAsync(int id);
        IQueryable<TEntity> GetQueryable();
        void Remove(TEntity entity);
        Task RemoveAsync(TEntity entity);
        void RemoveRange(List<TEntity> entities);
        Task RemoveRangeAsync(List<TEntity> entities);
        TEntity Update(TEntity entity);
        Task<TEntity> UpdateAsync(TEntity entity);
        void UpdateRange(List<TEntity> entities);
        Task UpdateRangeAsync(List<TEntity> entities);
    }
}