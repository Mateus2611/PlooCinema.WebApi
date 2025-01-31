using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PlooCinema.WebApi.Models;
using PlooCinema.WebApi.Models.DTOs;
using PlooCinema.WebApi.Models.Responses;

namespace PlooCinema.WebApi.Services.Interfaces
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