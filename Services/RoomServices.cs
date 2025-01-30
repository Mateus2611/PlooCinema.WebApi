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
        
        public async Task<RoomResponse> CreateAsync(RoomDTO room)
        {
            var createObject = mapper.Map<Room>(room);

            return 
                mapper.Map<RoomResponse>
                (
                    await roomRepository.CreateAsync(createObject)
                );
        }

        public async Task DeleteAsync(Guid id)
        {
            var room = await roomRepository.GetByIdAsync(id) ?? throw new KeyNotFoundException(id.ToString());
            await roomRepository.DeleteAsync(room);
        }

        public async Task<IEnumerable<RoomResponse>> GetAllAsync()
        {
            return 
                mapper.Map<IEnumerable<RoomResponse>>
                (
                    await roomRepository.GetAllAsync()
                );
        }

        public async Task<RoomResponse> GetByIdAsync(Guid id)
        {
            return 
                mapper.Map<RoomResponse>
                (
                    await roomRepository.GetByIdAsync(id)
                );
        }

        public async Task<IEnumerable<RoomResponse>> GetByNameAsync(string name)
        {
            return 
                mapper.Map<IEnumerable<RoomResponse>>
                (
                    await roomRepository.GetByNameAsync(name)
                );
        }

        public async Task<RoomResponse> UpdateAsync(Guid id, RoomDTO room)
        {
            var updatedValue = mapper.Map<Room>(room);
            updatedValue.Id = id;

            return
                mapper.Map<RoomResponse>
                (
                    await roomRepository.UpdateAsync(updatedValue)
                );
        }
    }
}