using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace PlooCinema.WebApi.Model
{
    public class Movie
    {
        public Movie(int id, string name, string genre, int duration, DateTime release, string description)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(name);
            ArgumentException.ThrowIfNullOrWhiteSpace(genre);
            ArgumentException.ThrowIfNullOrWhiteSpace(description);
            Id = id;
            Name = name;
            Genre = genre;
            Duration = duration;
            Release = release;
            Description = description;
        }
        public Movie(string name, string genre, int duration, DateTime release, string description)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(name);
            ArgumentException.ThrowIfNullOrWhiteSpace(genre);
            ArgumentException.ThrowIfNullOrWhiteSpace(description);
            Name = name;
            Genre = genre;
            Duration = duration;
            Release = release;
            Description = description;
        }
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Genre { get; set; }
        private int _duration;
        public int Duration
        {
            get => _duration;
            set
            {
                if (value <= 0)
                    throw new ArgumentException("Informe a duração do filme.");

                _duration = value;
            }
        }
        private DateTime _release;
        public DateTime Release
        {
            get => _release;
            set
            {
                if (value > DateTime.Now.Date)
                    throw new ArgumentException("A data informada não é valida.");

                _release = value.Date;
            }
        }
        [Required]
        public string Description { get; set;}

        public override string ToString()
        {
            return $"ID: {Id} Nome: {Name}, Gênero: {Genre}, Duração: {Duration}, Lançamento: {Release}, Descrição: {Description}\n";
        }

    }
}