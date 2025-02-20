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
        Task<SessionResponse?> CreateAsync(SessionDTO session);
        Task<IEnumerable<SessionResponse>> GetAllAsync(int skip, int take);
        Task<SessionResponse?> UpdateAsync(Guid id, SessionDTO session);
        Task DeleteAsync(Guid id);
        Task<SessionResponse> GetByIdAsync(Guid id);
        Task<IEnumerable<SessionResponse>> GetSessionsFilteredByMovieAndRoomAsync(Guid? movieId, Guid? roomId, int skip, int take);
        Task<SessionResponse> ReserveSeatsAsync(Guid id, int seats);
        Task<SessionResponse> CancelReservedSeatsAsync(Guid id, int seats);
    }
}