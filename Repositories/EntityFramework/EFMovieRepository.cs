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

        // Addgenre funcionando, falta concertar o RemoveGenre
        public new Movie? Update(Movie movie)
        {
            context.Entry(movie).State = EntityState.Modified;

            foreach (var genre in movie.Genres)
            {
                context.Genres.Update(genre);
            }

            context.SaveChanges();

            return movie;
        }
    }
}