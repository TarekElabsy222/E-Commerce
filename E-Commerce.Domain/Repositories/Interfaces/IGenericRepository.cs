using System.Linq.Expressions;

namespace E_Commerce.Domain.Repositories.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>>? filter = null, string? includeProperties = null);
        Task<TEntity> GetSingleAsync(Expression<Func<TEntity, bool>>? filter = null, string? includeProperties = null);
        Task AddAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(Guid id);
    }
}
