using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using PlooCinema.WebApi.Model;

namespace PlooCinema.WebApi.Models
{
    public class Session
    {
        public Session() {}
        public Session(DateTimeOffset startMovie, int seatsAvailable, Movie movie, Room room)
        {
            StartMovie = startMovie;
            SeatsAvailable = seatsAvailable;
            Movies = movie;
            Rooms = room;
        }
        
        public Session(Guid id, DateTimeOffset startMovie, int seatsAvailable, Movie movie, Room room)
        {
            Id = id;
            StartMovie = startMovie;
            SeatsAvailable = seatsAvailable;
            Movies = movie;
            Rooms = room;
        }


        public Guid Id { get; set; }
        public DateTimeOffset StartMovie { get; set; }
        public int SeatsAvailable { get; set; }
        public virtual Movie Movies { get; set; }
        public virtual Room Rooms { get; set; }

        public void ReserveSeats(int seats)
        {
            if (SeatsAvailable - seats >= 0)
                SeatsAvailable -= seats;
            
            throw new Exception("A sala não possui assentos suficiente para essa reserva.");
        }

        public void CancelReservedSeats(int seats)
        {
            if (Rooms is not null && SeatsAvailable + seats <= Rooms.Seats)
                SeatsAvailable += seats;

            throw new Exception("A quantidade de assentos ultrapassam o limite suportado pela sala.");
        }
    }
}