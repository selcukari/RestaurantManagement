using Microsoft.EntityFrameworkCore;
using RestaurantManagement.Order.Application.Contracts.Repositories;
using RestaurantManagement.Order.Domain.Entities;
using System.Linq.Expressions;

namespace RestaurantManagement.Order.Persistence.Repositories
{
    public class GenericRepository<TId, TEntity>(AppDbContext context)
    : IGenericRepository<TId, TEntity> where TId : struct where TEntity : BaseEntity<TId>
    {
        private readonly DbSet<TEntity> _dbSet = context.Set<TEntity>();

        protected AppDbContext Context = context;


        public Task<bool> AnyAsync(TId id)
        {
            return _dbSet.AnyAsync(x => x.Id.Equals(id));
        }

        public Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbSet.AnyAsync(predicate);
        }

        public Task<List<TEntity>> GetAllAsync()
        {
            return _dbSet.ToListAsync();
        }

        public Task<List<TEntity>> GetAllPagedAsync(int pageNumber, int pageSize)
        {
            // 1,10 => 1..10
            // 2,10 => 11..20
            return _dbSet.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
        }

        public ValueTask<TEntity?> GetByIdAsync(TId id)
        {
            return _dbSet.FindAsync(id);
        }

        public async Task<TEntity?> GetByIdReadOnlyAsync(TId id)
        {
            return await _dbSet
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id!.Equals(id));
        }

        public IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbSet.Where(predicate);
        }

        public void Add(TEntity entity)
        {
            _dbSet.Add(entity);
        }

        public void Update(TEntity entity)
        {
            _dbSet.Update(entity);
        }

        public void Remove(TEntity entity)
        {
            _dbSet.Remove(entity);
        }
    }
}
