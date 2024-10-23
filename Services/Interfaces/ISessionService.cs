using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PlooCinema.WebApi.Models;
using PlooCinema.WebApi.Models.DTOs;
using PlooCinema.WebApi.Models.Responses;

namespace PlooCinema.WebApi.Services.Interfaces
{
    public interface ISessionService
    {
        SessionResponse? Create(SessionDTO session);
        IEnumerable<SessionResponse> GetAll();
        SessionResponse? Update(int id, SessionDTO session);
        void Delete(int id);
        SessionResponse? GetById(int id);
        Session? ReserveSeats(int id, int seats);
        Session? CancelReservedSeats(int id, int seats);
    }
}