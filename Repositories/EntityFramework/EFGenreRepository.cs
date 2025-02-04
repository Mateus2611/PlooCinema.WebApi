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
        private readonly IDbContextFactory<DataContext> _contextFactory;

        public EFGenreRepository(IDbContextFactory<DataContext> context) : base(context)
            => _contextFactory = context;

        public async Task<Genre?> GetByIdAsync(Guid id)
        {
            using var context = _contextFactory.CreateDbContext();

            Genre genre = await context.Genres
                .AsNoTracking()
                .Include( g => g.Movies)
                .SingleAsync(g => g.Id == id);

            return genre;
        }

        public async Task<IEnumerable<Genre>> GetByNameAsync(string name, int skip, int take)
        {
            using var context = _contextFactory.CreateDbContext();

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