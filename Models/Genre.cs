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

        public Genre(int id, string name, IEnumerable<Movie> movies)
        {
            Id = id;
            Name = name;
            Movies = movies;
        }

        public Genre(string name, IEnumerable<Movie> movies)
        {
            Name = name;
            Movies = movies;
        }

        public int Id { get; set; }
        [Required(ErrorMessage = "Informe o nome do gênero.")]
        public string Name { get; set; }
        public IEnumerable<Movie> Movies { get; set; }

        public void AddMovie(IEnumerable<Movie> movies)
        {
            IEnumerable<Movie> query = movies.Except(Movies);

            if (query != null)
            {
                foreach (Movie movie in query)
                {
                    Movies = Movies.Append(movie);
                }
            }
        }
    }
}