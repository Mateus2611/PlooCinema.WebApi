using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PlooCinema.WebApi.Models;

namespace PlooCinema.WebApi.Repositories
{
    public interface IRoomRepository
    {
        Room? Create(Room room);
        IEnumerable<Room> GetAll();
        Room? Update(Room room);
        void Delete(Room room);
        IEnumerable<Room> GetByName(string name);
        Room? GetById(int id);
    }
}