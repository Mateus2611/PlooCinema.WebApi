using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using PlooCinema.WebApi.Model;

namespace PlooCinema.WebApi.Models
{
    public class Room
    {
        public Room() {}

        public Room(int id, string name, int seats)
        {
            Id = id;
            Name = name;
            Seats = seats;
        }

        public int Id { get; set; }
        public required string Name { get; set; }
        public int Seats { get; set; }
        public virtual ICollection<Session> UpComingSessions { get; set; } = [];

        public bool BookRoom(Movie movie, DateTimeOffset dateStart)
        {
            DateTimeOffset endSession = dateStart.AddMinutes(movie.Duration);

            return UpComingSessions.Any( s => 
            (
                dateStart < s.StartMovie.AddMinutes(s.Movie.Duration) &&
                endSession > s.StartMovie    
            ));
        }
    }
}