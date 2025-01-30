using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using AutoMapper;
using PlooCinema.WebApi.Models;
using PlooCinema.WebApi.Models.DTOs;
using PlooCinema.WebApi.Models.Responses;
using PlooCinema.WebApi.Repositories;
using PlooCinema.WebApi.Services.Interfaces;

namespace PlooCinema.WebApi.Services
{
    public class SessionService(ISessionRepository sessionRepository, IMovieRepository movieRepository, IRoomRepository roomRepository, IMapper mapper) : ISessionService
    {
        private readonly ISessionRepository sessionRepository = sessionRepository;

        private readonly IMovieRepository movieRepository = movieRepository;

        private readonly IRoomRepository roomRepository = roomRepository;
        private readonly IMapper mapper = mapper;

        public async Task<SessionResponse?> CreateAsync(SessionDTO session)
        {
            var movie = await movieRepository.GetByIdAsync(session.MovieId);
            var room = await roomRepository.GetByIdAsync(session.RoomId);
            var createSession = mapper.Map<Session>(session);

            if (movie is null || room is null)
                return null;

            var validation = room.BookRoom(movie, session.StartMovie);

            if (validation is true)
                throw new Exception("Já existe uma sessão para este horário.");

            createSession.Movies = movie;
            createSession.Rooms = room;
            createSession.SeatsAvailable = room.Seats;

            return
                mapper.Map<SessionResponse>
                (
                    await sessionRepository.CreateAsync(createSession)
                );
        }

        public async Task DeleteAsync(Guid id)
        {
            var session = await sessionRepository.GetByIdAsync(id) ?? throw new KeyNotFoundException(id.ToString());
            await sessionRepository.DeleteAsync(session);
        }

        public async Task<SessionResponse?> UpdateAsync(Guid id, SessionDTO session)
        {
            var movie = await movieRepository.GetByIdAsync(session.MovieId);
            var room = await roomRepository.GetByIdAsync(session.RoomId);
            var updateSession = mapper.Map<Session>(session);

            if (movie is null || room is null)
                return null;

            var validation = room.BookRoom(movie, session.StartMovie);

            if (validation is true)
                throw new Exception("Já existe uma sessão para este horário.");

            updateSession.Id = id;
            updateSession.Movies = movie;
            updateSession.Rooms = room;
            updateSession.SeatsAvailable = room.Seats;

            return
                mapper.Map<SessionResponse>
                (
                    await sessionRepository.UpdateAsync(updateSession)
                );
        }

        public async Task<IEnumerable<SessionResponse>> GetAllAsync()
        {
            return
                mapper.Map<IEnumerable<SessionResponse>>
                (
                    await sessionRepository.GetAllAsync()
                );
        }

        public async Task<SessionResponse> GetByIdAsync(Guid id)
        {
            return
                mapper.Map<SessionResponse>
                (
                    await sessionRepository.GetByIdAsync(id)
                );
        }

        public async Task<SessionResponse> ReserveSeatsAsync(Guid id, int seats)
        {
            return
                mapper.Map<SessionResponse>
                (
                    await sessionRepository.ReserveSeatsAsync(id, seats)
                );
        }

        public async Task<SessionResponse> CancelReservedSeatsAsync(Guid id, int seats)
        {
            return
                mapper.Map<SessionResponse>
                (
                    await sessionRepository.CancelReservedSeatsAsync(id, seats)
                );
        }
    }
}