using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PlooCinema.WebApi.Model;

namespace PlooCinema.WebApi.Repositories.EntityFramework
{
    public class EFMovieRepository : EFGenericRepository<Movie>, IMovieRepository
    {
        private readonly DataContext context;

        public EFMovieRepository(DataContext context) : base(context)
            => this.context = context;

        public Movie? GetById(Guid id)
        {
            Movie movie = context.Movies
                .AsNoTracking()
                .Single(m => m.Id == id);

            return movie;
        }

        public IEnumerable<Movie> GetByName(string name)
            => context.Movies
                .AsNoTracking()
                .Where(m => m.Name.ToLower().Contains(name.ToLower()))
                .ToList();

        public new Movie? Create(Movie movie)
        {
            context.Movies.Add(movie);
            context.SaveChanges();
            context.Entry(movie).State = EntityState.Detached;

            return movie;
        }

        // Método Update com bug por causa de trackemanento, agora crash na ação de addGenre
        public new Movie? Update(Movie movie)
        {
            var trackedMovie = context.Movies.SingleOrDefault(m => m.Id == movie.Id);

            if (trackedMovie is not null)
                context.Entry(trackedMovie).State = EntityState.Detached;
            
            context.Movies.Update(movie);
            context.SaveChanges();
            context.Entry(movie).State = EntityState.Detached;
            
            return movie;
        }
    }
}