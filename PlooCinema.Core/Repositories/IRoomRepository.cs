using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using PlooCinema.Core.Models;

namespace PlooCinema.Core.Repositories
{
    public interface IRoomRepository
    {
        Task<Room> CreateAsync(Room room);
        Task<IEnumerable<Room>> GetAllAsync(int skip, int take);
        Task<Room>? UpdateAsync(Room room);
        Task DeleteAsync(Room room);
        Task<IEnumerable<Room>> GetByNameAsync(string name, int skip, int take);
        Task<Room?> GetByIdAsync(Guid id);
    }
}