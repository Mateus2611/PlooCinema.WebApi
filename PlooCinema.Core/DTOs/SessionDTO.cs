using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PlooCinema.Core.DTOs
{
    public class SessionDTO
    {
        public DateTimeOffset StartMovie 
        { 
            get => _startMovie; 
            set => _startMovie = value.UtcDateTime; 
        }
        private DateTimeOffset _startMovie { get; set; }
        public Guid MovieId { get; set; }
        public Guid RoomId { get; set; }
    }
}