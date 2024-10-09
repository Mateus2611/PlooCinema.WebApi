using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PlooCinema.WebApi.Models;
using PlooCinema.WebApi.Repositories;
using PlooCinema.WebApi.Services.Interfaces;

namespace PlooCinema.WebApi.Services
{
    public class RoomServices(IRoomRepository roomRepository) : IRoomServices
    {
        private readonly IRoomRepository roomRepository = roomRepository;
        public Room? Create(Room room)
        {
            return roomRepository.Create(room);
        }

        public void Delete(Room room)
        {
            roomRepository.Delete(room);
        }

        public IEnumerable<Room> GetAll()
        {
            return roomRepository.GetAll();
        }

        public Room? GetById(int id)
        {
            return roomRepository.GetById(id);
        }

        public IEnumerable<Room> GetByName(string name)
        {
            return roomRepository.GetByName(name);
        }

        public Room? Update(Room room)
        {
            return roomRepository.Update(room);
        }
    }
}