using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.Features;
using PlooCinema.WebApi.Model;

namespace PlooCinema.WebApi.Models
{
    public class Genre
    {
        public Genre() { }
        
        public Genre(int id, string name)
        {
             Id = id;
            Name = name;
        }

        public int Id { get; set; }
        [Required(ErrorMessage = "Informe o nome do gênero.")]
        public string Name { get; set; }
        IEnumerable<Movie> Movies { get; set; }
    }
}