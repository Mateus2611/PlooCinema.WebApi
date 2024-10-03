using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PlooCinema.WebApi.Models;

namespace PlooCinema.WebApi.Repositories.EntityFramework
{
    public class EFGenreRepository : EFGenericRepository<Genre>, IGenreRepository
    {
        private readonly DataContext context;

        public EFGenreRepository(DataContext context) : base(context) 
            => this.context = context;

        public Genre? GetById(int id)
            => context.Genres
                .AsNoTracking()
                .SingleOrDefault( g => g.Id == id);

        public IEnumerable<Genre> GetByName(string name)
            => context.Genres
                .AsNoTracking()
                .Where( g => g.Name.ToLower().Contains(name.ToLower()));
    }
}