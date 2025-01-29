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

        public async Task<Genre?> GetByIdAsync(Guid id)
        {
            Genre genre = await context.Genres
                .AsNoTracking()
                .SingleAsync( g => g.Id == id );
            
            return genre;
        }

        public async Task<IEnumerable<Genre>> GetByNameAsync(string name)
            => await context.Genres
                .AsNoTracking()
                .Where( g => g.Name.ToLower().Contains(name.ToLower()))
                .ToListAsync();
    }
}