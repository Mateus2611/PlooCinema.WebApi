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
        public Session() { }

        public Session(int id, DateTimeOffset startMovie, Movie movie, Room room)
        {
            Id = id;
            StartMovie = startMovie;
            Movie = movie;
            Room = room;
            _seatsAvailable = room.Seats;
        }

        public int Id { get; set; }
        [Required(ErrorMessage = "Informe a data e horário da sessão.")]
        public DateTimeOffset StartMovie { get; set; }
        public int SeatsAvailable
        {
            get => _seatsAvailable;
        }
        private int _seatsAvailable { get; set; }
        
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public required virtual Movie Movie { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public required virtual Room Room { get; set; }

        public void ReserveSeats(int seats)
        {
            if (_seatsAvailable - seats >= 0)
                _seatsAvailable -= seats;
            
            throw new Exception("A sala não possui assentos suficiente para essa reserva.");
        }

        public void CancelReservedSeats(int seats)
        {
            if (Room is not null && _seatsAvailable + seats <= Room.Seats)
                _seatsAvailable += seats;

            throw new Exception("A quantidade de assentos ultrapassam o limite suportado pela sala.");
        }
    }
}