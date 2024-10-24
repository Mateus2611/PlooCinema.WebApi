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
        public int Id { get; set; }
        public DateTimeOffset StartMovie { get; set; }
        public int SeatsAvailable { get; set; }
        public required virtual Movie Movie { get; set; }
        public required virtual Room Room { get; set; }

        public void ReserveSeats(int seats)
        {
            if (SeatsAvailable - seats >= 0)
                SeatsAvailable -= seats;
            
            throw new Exception("A sala não possui assentos suficiente para essa reserva.");
        }

        public void CancelReservedSeats(int seats)
        {
            if (Room is not null && SeatsAvailable + seats <= Room.Seats)
                SeatsAvailable += seats;

            throw new Exception("A quantidade de assentos ultrapassam o limite suportado pela sala.");
        }
    }
}