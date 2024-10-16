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

        public Movie? GetById(int id)
            => context.Movies
                .Find(id);

        public IEnumerable<Movie> GetByName(string name)
            => context.Movies
                .AsNoTracking()
                .Where( m => m.Name.ToLower().Contains(name.ToLower()))
                .ToList(); 
    }
}