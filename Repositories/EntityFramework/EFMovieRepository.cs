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
        private readonly DataContext context;

        public EFMovieRepository(DataContext context) : base(context)
            => this.context = context;

        public async Task<Movie?> GetByIdAsync(Guid id)
            => await context.Movies.FindAsync(id);

        public async Task<IEnumerable<Movie>> GetByNameAsync(string name)
            => await context.Movies
                .Where(m => m.Name.ToLower().Contains(name.ToLower()))
                .ToListAsync();

        public async new Task<Movie> CreateAsync(Movie movie)
        {
            await context.Movies.AddAsync(movie);
            await context.SaveChangesAsync();
            context.Entry(movie).State = EntityState.Detached;

            return movie;
        }
    }
}