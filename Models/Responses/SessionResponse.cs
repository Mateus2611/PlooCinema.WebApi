using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PlooCinema.WebApi.Model;

namespace PlooCinema.WebApi.Models.Responses
{
    public class SessionResponse
    {
        public Guid Id { get; set; }
        public DateTimeOffset StartMovie 
        { 
            get => _startMovie.ToLocalTime(); 
            set => _startMovie = value; 
        }
        private DateTimeOffset _startMovie { get; set; }
        public int SeatsAvailable { get; set; }
        public required virtual MovieSessionResponse Movies { get; set; }
        public required virtual RoomSessionResponse Rooms { get; set; }
    }
}