using E_Commerce.Domain.Repositories.Interfaces;
using E_Commerce.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace E_Commerce.Infrastructure.Repositories.Implementations
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class , IEntity
    {
        #region Field
        protected readonly AppDbContext _context;
        protected readonly DbSet<TEntity> _dbSet;
        #endregion

        #region Constructor
        public GenericRepository(AppDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }
        #endregion

        #region Handle function        
        public async Task AddAsync(TEntity entity) => await _dbSet.AddAsync(entity);

        public async Task DeleteAsync(Guid id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity != null)
                _dbSet.Remove(entity);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>>? filter = null, string? includeProperties = null)
        {
            IQueryable<TEntity> query = _dbSet;
            if (filter != null)
                query = query.Where(filter);
            if(includeProperties != null)
            {
                foreach(var item in includeProperties.Split(new char[] {','}, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(item);
                }
            }
            return await query.AsNoTracking().ToListAsync();
        }

        public async Task<TEntity> GetSingleAsync(Expression<Func<TEntity, bool>>? filter = null, string? includeProperties = null)
        {
            IQueryable<TEntity> query = _dbSet;
            if (filter != null)
                query = query.Where(filter);
            if (includeProperties != null)
            {
                foreach (var item in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(item);
                }
            }
            return await query.AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task UpdateAsync(TEntity entity) 
        {
            var existItem = await _context.Set<TEntity>().FindAsync(entity.Id);
            if (existItem != null)
            {
                _context.Entry(existItem).CurrentValues.SetValues(entity);
            }
        }
        
        #endregion
    }
}
