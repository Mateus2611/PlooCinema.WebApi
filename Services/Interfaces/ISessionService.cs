using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PlooCinema.WebApi.Models;

namespace PlooCinema.WebApi.Services.Interfaces
{
    public interface ISessionService
    {
        Session? Create(Session session, int idMovie, int idRoom);
        IEnumerable<Session> GetAll();
        Session? Update(Session session);
        void Delete(Session session);
        Session? GetById(int id);
        Session? ReserveSeats(int id, int seats);
        Session? CancelReservedSeats(int id, int seats);
    }
}