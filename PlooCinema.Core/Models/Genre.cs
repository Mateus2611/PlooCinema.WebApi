using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PlooCinema.Core.Models
{
    public class Genre
    {
        public Genre() {}
        public Genre(Guid id, string name)
        {
            Id = id;
            Name = name;
        }

        public Guid Id { get; set; }
        public required string Name { get; set; }
        [JsonIgnore]
        public ICollection<Movie> Movies { get; set; } = [];
    }
}