using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PlooCinema.Core.Models;
using PlooCinema.Core.Repositories;

namespace PlooCinema.Infrastructure.Data.Repositories
{
    public class EFGenreRepository : EFGenericRepository<Genre>, IGenreRepository
    {
        private readonly DataContext context;

        public EFGenreRepository(DataContext context) : base(context) => this.context = context;

        public async Task<Genre?> GetByIdAsync(Guid id)
        {
            Genre genre = await context.Genres
                .AsNoTracking()
                .Include( g => g.Movies)
                .SingleAsync(g => g.Id == id);

            return genre;
        }

        public async Task<IEnumerable<Genre>> GetByNameAsync(string name, int skip, int take)
        {
            return await context.Genres
                    .AsNoTracking()
                    .Include(g => g.Movies)
                    .Where(g => g.Name.ToLower().Contains(name.ToLower()))
                    .Skip(skip)
                    .Take(take)
                    .ToListAsync();
        }
    }
}