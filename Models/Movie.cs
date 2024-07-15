using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PlooCinema.WebApi.Model
{
    public class Movie
    {
        public Movie() {}
        public Movie(int id, string name, string genre, int duration, DateTime release, string description)
        {
            Id = id;
            Name = name;
            Genre = genre;
            Duration = duration;
            Release = release;
            Description = description;
        }
        public Movie(string name, string genre, int duration, DateTime release, string description)
        {
            Name = name;
            Genre = genre;
            Duration = duration;
            Release = release;
            Description = description;
        }

        public int Id { get; set; }
        [Required(ErrorMessage = "Informe o nome do filme.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Informe o genêro do filme.")]
        public string Genre { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Duração inválida. Informe um valor acima de zero")]
        public int Duration { get; set; }
        public DateTime Release
        {
            get => Release;
            set
            {
                if (value > DateTime.Now.Date)
                    throw new ArgumentException("A data informada não é valida.");

                value = value.Date;
            }
        }
        [Required(ErrorMessage = "Informe a descrição do filme.")]
        public string Description { get; set; }

        public override string ToString()
        {
            return $"ID: {Id} Nome: {Name}, Gênero: {Genre}, Duração: {Duration}, Lançamento: {Release}, Descrição: {Description}\n";
        }

    }
}