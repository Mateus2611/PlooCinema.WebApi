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

        public EFSessionRepository( DataContext context) : base(context)
            => this.context = context;

        public Session? GetById(Guid id)
        {
            Session session = context.Sessions
                .AsNoTracking()
                .Single(s => s.Id == id);
            
            return session;
        }

        public Session? ReserveSeats(Guid id, int seats)
        {
            var session = GetById(id);

            if (session is null)
                return null;

            session.ReserveSeats(seats);
            return Update(session);
        }

        public Session? CancelReservedSeats(Guid id, int seats)
        {
            var session = GetById(id);

            if (session is null)
                return null;
            
            session.CancelReservedSeats(seats);
            return Update(session);
        }
    }
}