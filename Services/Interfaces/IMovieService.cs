using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PlooCinema.WebApi.Model;

namespace PlooCinema.WebApi.Services.Interfaces
{
    public interface IMovieService
    {
        Movie? Create(Movie movie);
        IEnumerable<Movie> GetAll();
        Movie? Update(Movie movie);
        void Delete(Movie movie);
        IEnumerable<Movie> GetByName(string name);
        Movie? GetById(int id);
        Movie? AddGenre(int idMovie, int idGenre);
        Movie? RemoveGenre(int idMovie, int idGenre);
    }
}