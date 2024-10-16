// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Runtime.CompilerServices;
// using System.Threading.Tasks;
// using PlooCinema.WebApi.Models;
// using PlooCinema.WebApi.Repositories;
// using PlooCinema.WebApi.Services.Interfaces;

// namespace PlooCinema.WebApi.Services
// {
//     public class SessionService(ISessionRepository sessionRepository, IMovieRepository movieRepository, IRoomRepository roomRepository) : ISessionService
//     {
//         private readonly ISessionRepository sessionRepository = sessionRepository;

//         private readonly IMovieRepository movieRepository = movieRepository;

//         private readonly IRoomRepository roomRepository = roomRepository;

//         public Session? Create(Session session, int idMovie, int idRoom)
//         {
//             var movie = movieRepository.GetById(idMovie);
//             var room = roomRepository.GetById(idRoom);

//             if ( movie is null || room is null)
//                 return null;

//             var validation = room.BookRoom(movie, session.StartMovie);

//             if (!validation)
//                 throw new Exception("Já existe uma sessão para este horário.");

//             session.Movie = movie;
//             session.Room = room;

//             return sessionRepository.Create(session);
//         }

//         public void Delete(Session session)
//         {
//             sessionRepository.Delete(session);
//         }

//         public Session? Update(Session session)
//         {
//             return sessionRepository.Update(session);
//         }

//         public IEnumerable<Session> GetAll()
//         {
//             return sessionRepository.GetAll();
//         }

//         public Session? GetById(int id)
//         {
//             return sessionRepository.GetById(id);
//         }

//         public Session? ReserveSeats(int id, int seats)
//         {
//             return sessionRepository.ReserveSeats(id, seats);
//         }

//         public Session? CancelReservedSeats(int id, int seats)
//         {
//             return sessionRepository.CancelReservedSeats(id, seats);
//         }
//     }
// }