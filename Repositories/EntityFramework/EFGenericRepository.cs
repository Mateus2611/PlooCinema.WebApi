using Microsoft.EntityFrameworkCore;
using PlooCinema.WebApi.Model;
using PlooCinema.WebApi.Repositories;

namespace PlooCinema.WebApi.Repositories.EntityFramework
{
    public class EFGenericRepository<T> where T : class
    {
        private readonly DataContext context;

        public EFGenericRepository(DataContext context) => this.context = context;

        public async Task<IEnumerable<T>> GetAllAsync(int skip, int take)
        {
            var itens = await context.Set<T>()
                        .AsNoTracking()
                        .Skip(skip)
                        .Take(take)
                        .ToListAsync();
            
            return itens;
        }

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
