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

        public Room? GetById(Guid id)
        {
            Room room = context.Rooms
                .AsNoTracking()
                .Single(r => r.Id == id);
            
            return room;
        }

        public IEnumerable<Room> GetByName(string name)
            => context.Rooms
                .AsNoTracking()
                .Where( r => r.Name.ToLower().Contains(name.ToLower()))
                .ToList();
    }
}