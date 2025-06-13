using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PlooCinema.Core.Models
{
    public class Room
    {
        public Room() {}

        public Room(Guid id, string name, int seats)
        {
            Id = id;
            Name = name;
            Seats = seats;
        }

        public Guid Id { get; set; }
        public required string Name { get; set; }
        public int Seats { get; set; }
        [JsonIgnore]
        public ICollection<Session> Sessions { get; set; } = [];

        public bool BookRoom(Movie movie, DateTimeOffset dateStart)
        {
            DateTimeOffset endSession = dateStart.AddMinutes(movie.Duration);

            return Sessions.Any( s => 
            
                dateStart < s.StartMovie.AddMinutes(s.Movies.Duration) &&
                endSession > s.StartMovie    
            );
        }
    }
}