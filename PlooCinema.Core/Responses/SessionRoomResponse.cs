using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlooCinema.Core.Responses
{
    public class SessionRoomResponse
    {
        public Guid Id { get; set; }
        public DateTimeOffset StartMovie 
        { 
            get => _startMovie.ToLocalTime(); 
            set => _startMovie = value; 
        }
        private DateTimeOffset _startMovie { get; set; }
    }
}