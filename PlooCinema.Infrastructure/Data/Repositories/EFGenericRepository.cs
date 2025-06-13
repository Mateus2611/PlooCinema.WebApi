using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace PlooCinema.Infrastructure.Data.Repositories
{
    public class EFGenericRepository<T> where T : class
    {
        private readonly DataContext context;

        public EFGenericRepository(DataContext context) => this.context = context;

        public async Task<IEnumerable<T>> GetAllAsync(int skip, int take)
        => await context.Set<T>()
            .AsNoTracking()
            .Skip(skip)
            .Take(take)
            .ToListAsync();

        public async Task<T> CreateAsync(T entity)
        {
            await context.Set<T>()
            .AddAsync(entity);
            await context.SaveChangesAsync();
            return entity;
        }

        public async Task<T> UpdateAsync(T entity)
        {
            context.Set<T>()
                .Update(entity);
            await context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(T entity)
        {
            context.Set<T>().Remove(entity);
            await context.SaveChangesAsync();
        }
    }
}
