using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using PlooCinema.WebApi.Models;

namespace PlooCinema.WebApi.Model
{
    public class Movie
    {
        public Movie() {}
        public Movie(int id, string name, IEnumerable<Genre> genre, int duration, DateTime release, string description)
        {
            Id = id;
            Name = name;
            Genres = genre;
            Duration = duration;
            Release = release;
            Description = description;
        }
        public Movie(string name, IEnumerable<Genre> genre, int duration, DateTime release, string description)
        {
            Name = name;
            Genres = genre;
            Duration = duration;
            Release = release;
            Description = description;
        }

        public int Id { get; set; }
        [Required(ErrorMessage = "Informe o nome do filme.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Informe o genêro do filme.")]
        public IEnumerable<Genre> Genres { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Duração inválida. Informe um valor acima de zero")]
        public int Duration { get; set; }
        private DateTime _release {get; set;}
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
        [Required(ErrorMessage = "Informe a descrição do filme.")]
        public string Description { get; set; }

        public void AddGenre(IEnumerable<Genre> genres)
        {
            var query = genres.Except(Genres);

            if (query != null)
            {
                foreach (Genre genre in query)
                {
                    Genres = Genres.Append(genre);
                }
            }
        }

        public override string ToString()
        {
            return $"ID: {Id} Nome: {Name}, Gênero: {Genres}, Duração: {Duration}, Lançamento: {Release}, Descrição: {Description}\n";
        }

    }
}