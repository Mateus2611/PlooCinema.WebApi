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

        public Movie? GetById(Guid id)
        {
            Movie? movie = context.Movies
                .Find(id);

            return movie;
        }

        public IEnumerable<Movie> GetByName(string name)
            => context.Movies
                .Where(m => m.Name.ToLower().Contains(name.ToLower()))
                .ToList();

        public new Movie? Create(Movie movie)
        {
            context.Movies.Add(movie);
            context.SaveChanges();
            context.Entry(movie).State = EntityState.Detached;

            return movie;
        }
    }
}