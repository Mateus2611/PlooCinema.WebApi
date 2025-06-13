using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PlooCinema.Core.Models
{
    public class Movie
    {
        public Movie() {}
        public Movie(Guid id, string name, int duration, DateTimeOffset release, string description)
        {
            Id = id;
            Name = name;
            Duration = duration;
            Release = release;
            Description = description;
        }

        public Guid Id { get; set; }
        public required string Name { get; set; }
        public int Duration { get; set; }
        public DateTimeOffset Release { get; set; }
        public required string Description { get; set; }
        public ICollection<Genre> Genres { get; set; } = [];
        [JsonIgnore]
        public ICollection<Session> Sessions{ get; set; } = [];
    }
}