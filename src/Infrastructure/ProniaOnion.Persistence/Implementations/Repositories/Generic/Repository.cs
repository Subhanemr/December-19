using Microsoft.EntityFrameworkCore;
using ProniaOnion.Application.Abstractions.Repositories;
using ProniaOnion.Domain.Entities;
using ProniaOnion.Persistence.Contexts;
using System.Linq.Expressions;

namespace ProniaOnion.Persistence.Implementations.Repositories.Generic
{
    public class Repository<T> : IRepository<T> where T : BaseEntity, new()
    {
        private readonly DbSet<T> _dbSet;
        private readonly AppDbContext _context;

        public Repository(AppDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public async Task<bool> CheckUnique(Expression<Func<T, bool>> expression)
        {
            bool result = await _dbSet.AnyAsync(expression);
            return result;
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        public IQueryable<T> GetAllAsync(Expression<Func<T, bool>>? expression = null, int skip = 0, int take = 0, bool IsTracking = true, bool IsDeleted = false, params string[] includes)
        {
            var query = _dbSet.AsQueryable();
            if (expression != null) query = query.Where(expression);

            if (skip != 0) query = query.Skip(skip);

            if (take != 0) query = query.Take(take);

            if (includes != null)
            {
                for (int i = 0; i < includes.Length; i++)
                {
                    query = query.Include(includes[i]);
                }
            }

            if (IsDeleted) query = query.IgnoreQueryFilters();

            return IsTracking ? query : query.AsNoTracking();
        }

        public IQueryable<T> GetAllByOrderAsync(Expression<Func<T, bool>>? expression = null, Expression<Func<T, object>>? orderException = null, bool IsDescending = false, int skip = 0, int take = 0, bool IsTracking = true, bool IsDeleted = false, params string[] includes)
        {
            var query = _dbSet.AsQueryable();
            if (expression != null) query = query.Where(expression);

            if (orderException != null)
            {
                if (IsDescending) query = query.OrderByDescending(orderException);
                else query = query.OrderBy(orderException);
            }

            if (skip != 0) query = query.Skip(skip);

            if (take != 0) query = query.Take(take);

            if (includes != null)
            {
                for (int i = 0; i < includes.Length; i++)
                {
                    query = query.Include(includes[i]);
                }
            }

            if (IsDeleted) query = query.IgnoreQueryFilters();

            return IsTracking ? query : query.AsNoTracking();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            T entity = await _dbSet.FirstOrDefaultAsync(x => x.Id == id);

            return entity;
        }

        public async Task SaveChanceAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void SoftDelete(T entity)
        {
            entity.IsDeleted = true;
            _dbSet.Update(entity);
        }

        public async void Update(T entity)
        {
            _dbSet.Update(entity);
        }
    }
}
