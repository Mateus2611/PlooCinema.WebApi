using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using PlooCinema.WebApi.Models;

namespace PlooCinema.WebApi.Model
{
    public class Movie
    {
        public Movie() {}
        public Movie(int id, string name, int duration, DateTimeOffset release, string description)
        {
            Id = id;
            Name = name;
            Duration = duration;
            Release = release;
            Description = description;
        }

        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Duration { get; set; }
        public DateTimeOffset Release { get; set; }
        public string Description { get; set; } = string.Empty;
        public virtual ICollection<Genre> Genres { get; set; } = [];
        [JsonIgnore]
        public virtual ICollection<Session> Sessions{ get; set; } = [];

        public override string ToString()
        {
            return $"ID: {Id} Nome: {Name}, Gênero: {Genres}, Duração: {Duration}, Lançamento: {Release}, Descrição: {Description}\n";
        }
    }
}