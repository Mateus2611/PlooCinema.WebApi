using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PlooCinema.WebApi.Models;

namespace PlooCinema.WebApi.Repositories.EntityFramework
{
    public class EFRoomRepository : EFGenericRepository<Room>, IRoomRepository
    {
        private readonly IDbContextFactory<DataContext> _contextFactory;

        public EFRoomRepository(IDbContextFactory<DataContext> context) : base(context)
            => _contextFactory = context;

        public async Task<Room?> GetByIdAsync(Guid id)
        {
            using var context = _contextFactory.CreateDbContext();

            return await context.Rooms
                .AsNoTracking()
                .Include(r => r.Sessions)
                .SingleAsync(g => g.Id == id);
        }

        public async Task<IEnumerable<Room>> GetByNameAsync(string name, int skip, int take)
        {
            using var context = _contextFactory.CreateDbContext();

            return await context.Rooms
                    .AsNoTracking()
                    .Include(r => r.Sessions)
                    .Where(g => g.Name.ToLower().Contains(name.ToLower()))
                    .Skip(skip)
                    .Take(take)
                    .ToListAsync();
        }
    }
}