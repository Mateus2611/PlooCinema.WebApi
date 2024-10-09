using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

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
        public string Name { get; set; }
        public int Seats { get; set; }
        [JsonIgnore]
        public virtual ICollection<Session> UpComingSessions { get; set; } = [];
    }
}