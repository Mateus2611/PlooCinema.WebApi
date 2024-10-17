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
        RoomResponse? Create(RoomDTO room);
        IEnumerable<RoomResponse> GetAll();
        RoomResponse? Update(int id, RoomDTO room);
        void Delete(int id);
        IEnumerable<RoomResponse> GetByName(string name);
        RoomResponse? GetById(int id);
    }
}