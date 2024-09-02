using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PlooCinema.WebApi.Model;
using PlooCinema.WebApi.Models;

namespace PlooCinema.WebApi.Repositories
{
    public interface IGenreRepository
    {
        Genre? Create (string name, IEnumerable<int> idMovies);
        Genre? AddMovies (IEnumerable<int> idMovie, int idGenre);
        IEnumerable<Genre> SearchAll();
        IEnumerable<Genre> SearchByName(string name);
        Genre? SearchById(int id);
        // Genre? SearchMovies();
        Genre? Update(int id, string name);
        void Delete(int id);
    }
}