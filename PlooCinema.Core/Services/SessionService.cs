using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using AutoMapper;
using PlooCinema.Core.DTOs;
using PlooCinema.Core.Models;
using PlooCinema.Core.Repositories;
using PlooCinema.Core.Responses;
using PlooCinema.Core.Services.Interfaces;

namespace PlooCinema.Core.Services
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

        public async Task<IEnumerable<SessionResponse>> GetAllAsync(int skip, int take)
        {
            return
                mapper.Map<IEnumerable<SessionResponse>>
                (
                    await sessionRepository.GetAllAsync(skip, take)
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

        public async Task<IEnumerable<SessionResponse>> GetSessionsFilteredByMovieAndRoomAsync(Guid? movieId, Guid? roomId, int skip, int take)
        {
            return
                   mapper.Map<IEnumerable<SessionResponse>>
                   (
                       await sessionRepository.GetSessionsFilteredByMovieAndRoomAsync(movieId, roomId, skip, take)
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