using Microsoft.EntityFrameworkCore;
using PlooCinema.WebApi.Model;
using PlooCinema.WebApi.Repositories;

namespace PlooCinema.WebApi.Repositories.EntityFramework
{
    public class EFGenericRepository<T> where T : class
    {
        private readonly DataContext context;

        public EFGenericRepository(DataContext context) => this.context = context;

        public IEnumerable<T> GetAll()
            => context.Set<T>()
                .AsNoTracking()
                .ToList();

        public T? Create(T entity)
        {
            context.Set<T>().Add(entity);
            context.SaveChanges();
            return entity;
        }

        public T? Update(T entity)
        {
            context.Set<T>()
                .Update(entity);
            context.SaveChanges();
            return entity;
        }

        public void Delete(T entity)
        {
            context.Set<T>().Remove(entity);
            context.SaveChanges();
        }
    }
}
