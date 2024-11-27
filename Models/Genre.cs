using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.Features;
using PlooCinema.WebApi.Model;

namespace PlooCinema.WebApi.Models
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
        public virtual ICollection<Movie> Movies { get; set; } = [];
    }
}