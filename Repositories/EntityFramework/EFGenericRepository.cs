using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using PlooCinema.WebApi.Model;
using PlooCinema.WebApi.Repositories;

namespace PlooCinema.WebApi.Repositories.EntityFramework
{
    public class EFGenericRepository<T> where T : class
    {
        private readonly IDbContextFactory<DataContext> _contextFactory;

        public EFGenericRepository(IDbContextFactory<DataContext> context) => _contextFactory = context;

        public async Task<IEnumerable<T>> GetAllAsync(int skip, int take, params Expression<Func<T, object>>[] includeProperties)
        {
            using var context = _contextFactory.CreateDbContext();
            IQueryable<T> query = context.Set<T>().AsNoTracking();

            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }

            return await query
                    .Skip(skip)
                    .Take(take)
                    .ToListAsync();
        }

        public async Task<T> CreateAsync(T entity)
        {
            using var context = _contextFactory.CreateDbContext();

            await context.Set<T>()
            .AddAsync(entity);
            await context.SaveChangesAsync();
            return entity;
        }

        public async Task<T> UpdateAsync(T entity)
        {
            using var context = _contextFactory.CreateDbContext();
            
            context.Set<T>()
                .Update(entity);
            await context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(T entity)
        {
            using var context = _contextFactory.CreateDbContext();
            
            context.Set<T>().Remove(entity);
            await context.SaveChangesAsync();
        }
    }
}
