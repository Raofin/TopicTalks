using System.Linq.Expressions;

namespace TopicTalks.Domain.Interfaces.Repositories;

public interface IRepository<TEntity> where TEntity : class
{
    Task<TEntity?> GetAsync(long id);
    Task<IEnumerable<TEntity>> GetAsync();

    Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate);
    Task<TEntity?> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);

    Task AddAsync(TEntity entity);
    Task AddAsync(IEnumerable<TEntity> entities);

    void Remove(TEntity entity);
    void Remove(IEnumerable<TEntity> entities);
}