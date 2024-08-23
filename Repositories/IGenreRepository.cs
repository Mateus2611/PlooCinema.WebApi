using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PlooCinema.WebApi.Models;

namespace PlooCinema.WebApi.Repositories
{
    public interface IGenreRepository
    {
        Genre? Create (Genre genre);
        // IEnumerable<Genre> SearchAll();
        // IEnumerable<Genre> SearchByName(string name);
        Genre? SearchById(int id);
        // Genre? SearchMovies();
        // Genre? Update(int id, Genre genre);
        // void Delete(int id);
    }
}