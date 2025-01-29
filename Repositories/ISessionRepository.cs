using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PlooCinema.WebApi.Models;

namespace PlooCinema.WebApi.Repositories
{
    public interface ISessionRepository
    {
        Task<Session> CreateAsync(Session session);
        Task<IEnumerable<Session>> GetAllAsync();
        Task<Session>? UpdateAsync(Session session);
        Task DeleteAsync(Session session);
        Task<Session?> GetByIdAsync(Guid id);
        Task<Session?> ReserveSeatsAsync(Guid id, int seats);
        Task<Session?> CancelReservedSeatsAsync(Guid id, int seats);
    }
}