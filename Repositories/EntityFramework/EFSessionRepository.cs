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
        private readonly IDbContextFactory<DataContext> _contextFactory;

        public EFSessionRepository( IDbContextFactory<DataContext> context) : base(context)
            => _contextFactory = context;

        public async Task<Session?> GetByIdAsync(Guid id)
        {
            using var context = _contextFactory.CreateDbContext();

            return await context.Sessions
                .FindAsync(id);
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