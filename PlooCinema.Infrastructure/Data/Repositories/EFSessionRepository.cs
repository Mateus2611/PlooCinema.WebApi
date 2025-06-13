using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PlooCinema.Core.Models;
using PlooCinema.Core.Repositories;

namespace PlooCinema.Infrastructure.Data.Repositories
{
    public class EFSessionRepository : EFGenericRepository<Session>, ISessionRepository
    {
        private readonly DataContext context;

        public EFSessionRepository(DataContext context) : base(context) => this.context = context;

        public async new Task<IEnumerable<Session>> GetAllAsync(int skip, int take)
            => await context.Sessions
            .AsNoTracking()
            .Include(s => s.Movies)
                .ThenInclude(s => s.Genres)
            .Include(s => s.Rooms)
            .Skip(skip)
            .Take(take)
            .ToListAsync();

        public async Task<Session?> GetByIdAsync(Guid id)
        {
            return await context.Sessions
                .AsNoTracking()
                .Include(s => s.Rooms)
                .Include(s => s.Movies)
                    .ThenInclude(s => s.Genres)
                .SingleAsync(g => g.Id == id);
        }

        public async Task<IEnumerable<Session>> GetSessionsFilteredByMovieAndRoomAsync(Guid? movieId, Guid? roomId, int skip, int take)
        {
            //if (movieId is not null && roomId is null)
            //{
            //    return await context.Sessions
            //        .AsNoTracking()
            //        .Include(s => s.Movies)
            //        .Where(s => s.Movies.Id == movieId)
            //        .Skip(skip)
            //        .Take(take)
            //        .ToListAsync();
            //}

            //if (roomId is not null && movieId is null)
            //{
            //    return await context.Sessions
            //        .AsNoTracking()
            //        .Include(s => s.Rooms)
            //        .Where(s => s.Rooms.Id == roomId)
            //        .Skip(skip)
            //        .Take(take)
            //        .ToListAsync();
            //}

            //return await context.Sessions
            //        .AsNoTracking()
            //        .Include(s => s.Movies)
            //        .Include(s => s.Rooms)
            //        .Where(s => s.Movies.Id == movieId && s.Rooms.Id == roomId)
            //        .Skip(skip)
            //        .Take(take)
            //        .ToListAsync();

            var query = context.Sessions.AsNoTracking();

            query = (movieId, roomId) switch
            {
                (not null, null) => query.Include(s => s.Movies)
                                         .ThenInclude(s => s.Genres)
                                         .Where(s => s.Movies.Id == movieId),

                (null, not null) => query.Include(s => s.Rooms)
                                         .Where(s => s.Rooms.Id == roomId),

                (not null, not null) => query.Include(s => s.Movies)
                                             .ThenInclude(s => s.Genres)
                                             .Include(s => s.Rooms)
                                             .Where(s => s.Movies.Id == movieId && s.Rooms.Id == roomId),
                _ => query
            };

            return await query.Skip(skip)
                              .Take(take)
                              .ToListAsync();
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