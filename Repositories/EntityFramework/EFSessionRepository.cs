using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using PlooCinema.WebApi.Models;

namespace PlooCinema.WebApi.Repositories.EntityFramework
{
    public class EFSessionRepository : EFGenericRepository<Session>, ISessionRepository
    {
        private readonly DataContext context;

        public EFSessionRepository(DataContext context) : base(context) => this.context = context;

        public async Task<Session?> GetByIdAsync(Guid id)
        {
            return await context.Sessions
                .AsNoTracking()
                .Include(s => s.Rooms)
                .Include(s => s.Movies)
                    .ThenInclude(s => s.Genres)
                .SingleAsync(g => g.Id == id);
        }

        public async Task<Session?> ReserveSeatsAsync(Guid id, int seats)
        {
            var session = await GetByIdAsync(id);

            if (session is null)
                return null;

            // await Task.Run(() => session.ReserveSeats(seats));
            session.ReserveSeats(seats);
            return await UpdateAsync(session);
        }

        public async Task<Session?> CancelReservedSeatsAsync(Guid id, int seats)
        {
            var session = await GetByIdAsync(id);

            if (session is null)
                return null;
            
            // await Task.Run(() => session.CancelReservedSeats(seats));
            session.CancelReservedSeats(seats);
            return await UpdateAsync(session);
        }
    }
}