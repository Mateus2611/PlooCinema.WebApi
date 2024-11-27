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
        SessionResponse? Update(Guid id, SessionDTO session);
        void Delete(Guid id);
        SessionResponse? GetById(Guid id);
        Session? ReserveSeats(Guid id, int seats);
        Session? CancelReservedSeats(Guid id, int seats);
    }
}