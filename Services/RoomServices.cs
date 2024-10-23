using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using PlooCinema.WebApi.Models;
using PlooCinema.WebApi.Models.DTOs;
using PlooCinema.WebApi.Models.Responses;
using PlooCinema.WebApi.Repositories;
using PlooCinema.WebApi.Services.Interfaces;

namespace PlooCinema.WebApi.Services
{
    public class RoomServices(IRoomRepository roomRepository, IMapper mapper) : IRoomServices
    {
        private readonly IRoomRepository roomRepository = roomRepository;
        private readonly IMapper mapper= mapper;
        
        public RoomResponse? Create(RoomDTO room)
        {
            var createObject = mapper.Map<Room>(room);

            return 
                mapper.Map<RoomResponse>
                (
                    roomRepository.Create(createObject)
                );
        }

        public void Delete(int id)
        {
            var room = roomRepository.GetById(id) ?? throw new KeyNotFoundException(id.ToString());
            roomRepository.Delete(room);
        }

        public IEnumerable<RoomResponse> GetAll()
        {
            return 
                mapper.Map<IEnumerable<RoomResponse>>
                (
                    roomRepository.GetAll()
                );
        }

        public RoomResponse? GetById(int id)
        {
            return 
                mapper.Map<RoomResponse>
                (
                    roomRepository.GetById(id)
                );
        }

        public IEnumerable<RoomResponse> GetByName(string name)
        {
            return 
                mapper.Map<IEnumerable<RoomResponse>>
                (
                    roomRepository.GetByName(name)
                );
        }

        public RoomResponse? Update(int id, RoomDTO room)
        {
            var updatedValue = mapper.Map<Room>(room);
            updatedValue.Id = id;

            return 
                mapper.Map<RoomResponse>
                (
                    roomRepository.Update(updatedValue)
                );
        }
    }
}