using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PlooCinema.Core.Models
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
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Movie Movies { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Room Rooms { get; set; }

        public void ReserveSeats(int seats)
        {
            if (SeatsAvailable - seats < 0)
                throw new Exception("A sala não possui assentos suficiente para essa reserva.");
            
            SeatsAvailable -= seats;
        }

        public void CancelReservedSeats(int seats)
        {
            if (SeatsAvailable + seats > Rooms.Seats)
                throw new Exception("A quantidade de assentos ultrapassam o limite suportado pela sala.");
            
            SeatsAvailable += seats;
        }
    }
}