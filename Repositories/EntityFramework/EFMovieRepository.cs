using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PlooCinema.WebApi.Model;
using PlooCinema.WebApi.Models;

namespace PlooCinema.WebApi.Repositories.EntityFramework
{
    public class EFMovieRepository : EFGenericRepository<Movie>, IMovieRepository
    {
        private readonly IDbContextFactory<DataContext> _contextFactory;

        public EFMovieRepository(IDbContextFactory<DataContext> context) : base(context)
            => _contextFactory = context;

        public async Task<Movie?> GetByIdAsync(Guid id)
        {
            using var context = _contextFactory.CreateDbContext();

            return await context.Movies.FindAsync(id);
        }

        public async Task<IEnumerable<Movie>> GetByNameAsync(string name, int skip, int take)
        {
            using var context = _contextFactory.CreateDbContext();

            return await context.Movies
               .AsNoTracking()
               .Where(m => m.Name.ToLower().Contains(name.ToLower()))
               .Skip(skip)
               .Take(take)
               .ToListAsync();
        }

        public async new Task<Movie> CreateAsync(Movie movie)
        {
            using var context = _contextFactory.CreateDbContext();
            
            await context.Movies.AddAsync(movie);
            await context.SaveChangesAsync();
            context.Entry(movie).State = EntityState.Detached;

            return movie;
        }
    }
}