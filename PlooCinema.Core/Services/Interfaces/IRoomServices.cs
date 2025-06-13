using PlooCinema.Core.DTOs;
using PlooCinema.Core.Responses;

namespace PlooCinema.Core.Services.Interfaces
{
    public interface IRoomServices
    {
        Task<RoomResponse> CreateAsync(RoomDTO room);
        Task<IEnumerable<RoomResponse>> GetAllAsync(int skip, int take);
        Task<RoomResponse> UpdateAsync(Guid id, RoomDTO room);
        Task DeleteAsync(Guid id);
        Task<IEnumerable<RoomResponse>> GetByNameAsync(string name, int skip, int take);
        Task<RoomResponse> GetByIdAsync(Guid id);
    }
}