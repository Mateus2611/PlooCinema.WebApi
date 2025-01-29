using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PlooCinema.WebApi.Models;

namespace PlooCinema.WebApi.Repositories
{
    public interface IRoomRepository
    {
        Task<Room> CreateAsync(Room room);
        Task<IEnumerable<Room>> GetAllAsync();
        Task<Room>? UpdateAsync(Room room);
        Task DeleteAsync(Room room);
        Task<IEnumerable<Room>> GetByNameAsync(string name);
        Task<Room?> GetByIdAsync(Guid id);
    }
}