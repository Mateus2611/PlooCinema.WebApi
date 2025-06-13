using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PlooCinema.Core.Models;
using PlooCinema.Core.Repositories;

namespace PlooCinema.Infrastructure.Data.Repositories
{
    public class EFRoomRepository : EFGenericRepository<Room>, IRoomRepository
    {
        private readonly DataContext context;

        public EFRoomRepository(DataContext context) : base(context) => this.context = context;

        public async Task<Room?> GetByIdAsync(Guid id)
        {
            return await context.Rooms.FindAsync(id);
        }

        public async Task<IEnumerable<Room>> GetByNameAsync(string name, int skip, int take)
        {
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