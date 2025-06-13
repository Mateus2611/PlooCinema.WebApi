using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PlooCinema.Core.Responses
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
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public required MovieSessionResponse Movies { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public required RoomSessionResponse Rooms { get; set; }
    }
}