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
        private readonly DataContext context;

        public EFRoomRepository(DataContext context) : base(context)
            => this.context = context;

        public async Task<Room?> GetByIdAsync(Guid id)
            => await context.Rooms.FindAsync(id);

        public async Task<IEnumerable<Room>> GetByNameAsync(string name)
            => await context.Rooms
                .AsNoTracking()
                .Where( r => r.Name.ToLower().Contains(name.ToLower()))
                .ToListAsync();
    }
}