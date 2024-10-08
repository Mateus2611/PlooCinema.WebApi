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
        public Movie(int id, string name, int duration, DateTime release, string description)
        {
            Id = id;
            Name = name;
            Duration = duration;
            Release = release;
            Description = description;
        }

        public int Id { get; set; }

        [Required(ErrorMessage = "Informe o nome do filme.")]
        public string Name { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Duração inválida. Informe um valor acima de zero")]
        public int Duration { get; set; }

        public DateTimeOffset Release
        {
            get => _release;
            set
            {
                if (value > DateTimeOffset.Now.Date)
                    throw new ArgumentException("A data informada não é valida.");

                _release = value.Date.ToUniversalTime();
            }
        }
        private DateTimeOffset _release { get; set; }

        [Required(ErrorMessage = "Informe a descrição do filme.")]
        public string Description { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public virtual ICollection<Genre> Genres { get; set; } = [];

        public override string ToString()
        {
            return $"ID: {Id} Nome: {Name}, Gênero: {Genres}, Duração: {Duration}, Lançamento: {Release}, Descrição: {Description}\n";
        }

    }
}