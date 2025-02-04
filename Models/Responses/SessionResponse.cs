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
        public required MovieSessionResponse Movies { get; set; }
        public required RoomSessionResponse Rooms { get; set; }
    }
}