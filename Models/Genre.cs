using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PlooCinema.WebApi.Model;

namespace PlooCinema.WebApi.Models
{
    public class Genre
    {
        public Genre() {}
        
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
        public string Name { get; set; }
        public IEnumerable<Movie> Movies { get; set; }

        public void AddMovies(IEnumerable<Movie> movies)
        {
            
        }
    }
}