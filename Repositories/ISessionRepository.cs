using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PlooCinema.WebApi.Models;

namespace PlooCinema.WebApi.Repositories
{
    public interface ISessionRepository
    {
        Session? Create(Session session);
        IEnumerable<Session> GetAll();
        Session? Update(Session session);
        void Delete(Session session);
        Session? GetById(Guid id);
        Session? ReserveSeats(Guid id, int seats);
        Session? CancelReservedSeats(Guid id, int seats);
    }
}