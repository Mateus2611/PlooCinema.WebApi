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

        public SessionResponse? Create(SessionDTO session)
        {
            var movie = movieRepository.GetById(session.MovieId);
            var room = roomRepository.GetById(session.RoomId);
            var createSession = mapper.Map<Session>(session);

            if ( movie is null || room is null)
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
                    sessionRepository.Create(createSession)
                );
        }

        public void Delete(Guid id)
        {
            var session = sessionRepository.GetById(id) ?? throw new KeyNotFoundException(id.ToString());
            sessionRepository.Delete(session);
        }

        public SessionResponse? Update(Guid id, SessionDTO session)
        {
            var movie = movieRepository.GetById(session.MovieId);
            var room = roomRepository.GetById(session.RoomId);
            var updateSession = mapper.Map<Session>(session);

            if ( movie is null || room is null)
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
                    sessionRepository.Update(updateSession)
                );
        }

        public IEnumerable<SessionResponse> GetAll()
        {
            return
                mapper.Map<IEnumerable<SessionResponse>>
                (
                    sessionRepository.GetAll()
                );
        }

        public SessionResponse? GetById(Guid id)
        {
            return 
                mapper.Map<SessionResponse>
                (
                    sessionRepository.GetById(id)
                );
        }

        public SessionResponse? ReserveSeats(Guid id, int seats)
        {
            return 
                mapper.Map<SessionResponse>
                (
                    sessionRepository.ReserveSeats(id, seats)
                );
        }

        public SessionResponse? CancelReservedSeats(Guid id, int seats)
        {
            return 
                mapper.Map<SessionResponse>
                (
                    sessionRepository.CancelReservedSeats(id, seats)
                );
        }
    }
}