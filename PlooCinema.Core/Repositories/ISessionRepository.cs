using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using PlooCinema.Core.Models;

namespace PlooCinema.Core.Repositories
{
    public interface ISessionRepository
    {
        Task<Session> CreateAsync(Session session);
        Task<IEnumerable<Session>> GetAllAsync(int skip, int take);
        Task<Session>? UpdateAsync(Session session);
        Task DeleteAsync(Session session);
        Task<Session?> GetByIdAsync(Guid id);
        Task<IEnumerable<Session>> GetSessionsFilteredByMovieAndRoomAsync(Guid? movieId, Guid? roomId, int skip, int take);
        Task<Session?> ReserveSeatsAsync(Guid id, int seats);
        Task<Session?> CancelReservedSeatsAsync(Guid id, int seats);
    }
}