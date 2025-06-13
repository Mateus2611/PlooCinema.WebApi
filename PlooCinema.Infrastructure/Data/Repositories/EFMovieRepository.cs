using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PlooCinema.Core.Models;
using PlooCinema.Core.Repositories;

namespace PlooCinema.Infrastructure.Data.Repositories
{
    public class EFMovieRepository : EFGenericRepository<Movie>, IMovieRepository
    {
        private readonly DataContext context;

        public EFMovieRepository(DataContext context) : base(context) => this.context = context;

        public async new Task<IEnumerable<Movie>> GetAllAsync(int skip, int take)
            => await context.Movies
            .AsNoTracking()
            .Include(m => m.Genres)
            .Skip(skip)
            .Take(take)
            .ToListAsync();

        public async Task<Movie?> GetByIdAsync(Guid id)
        {
            return await context.Movies.FindAsync(id);
        }

        public async Task<IEnumerable<Movie>> GetByNameAsync(string name, int skip, int take)
        {
            return await context.Movies
                    .AsNoTracking()
                    .Include(m => m.Genres)
                    .Where(g => g.Name.ToLower().Contains(name.ToLower()))
                    .Skip(skip)
                    .Take(take)
                    .ToListAsync();
        }

        public async new Task<Movie> CreateAsync(Movie movie)
        {
             _ = context.Movies.AddAsync(movie);
             _ = await context.SaveChangesAsync();
            context.Entry(movie).State = EntityState.Detached;

            return movie;
        }
    }
}