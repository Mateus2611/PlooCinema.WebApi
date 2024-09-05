using Microsoft.EntityFrameworkCore;
using PlooCinema.WebApi.Model;
using PlooCinema.WebApi.Repositories;

namespace PlooCinema.WebApi.Repositories.EntityFramework
{
    public class MovieRepositoryEntityFramework : IMovieRepository
    {
        private readonly DataContext _context;

        public MovieRepositoryEntityFramework(DataContext context)
        {
            _context = context;
        }

        public Movie? Create(Movie movie)
        {
            _context.Movies.Add(movie);
            _context.SaveChanges();
            return movie;
        }

        public void Delete(int id)
        {
            var movie = SearchById(id);
            if (movie != null)
            {
                _context.Movies.Remove(movie);
                _context.SaveChanges();
            }
        }

        public IEnumerable<Movie> SearchAll()
            => _context.Movies.ToList();

        public Movie? SearchById(int id)
            => _context.Movies.SingleOrDefault(m => m.Id == id);

        public IEnumerable<Movie> SearchByName(string name)
            => _context.Movies.Where(m => m.Name.ToLower().Contains(name.ToLower())).ToList();

        public Movie? Update(int id, Movie movie)
        {
            var movieToUpdate = SearchById(id);

            if (movieToUpdate != null)
            {
                movieToUpdate.Name = movie.Name;
                movieToUpdate.Description = movie.Description;
                movieToUpdate.Duration = movie.Duration;
                movieToUpdate.Release = movie.Release;

                _context.SaveChanges();
            }

            return movieToUpdate;
        }
    }
}
