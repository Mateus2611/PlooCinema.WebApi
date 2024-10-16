using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PlooCinema.WebApi.Model;
using PlooCinema.WebApi.Models.DTOs;

namespace PlooCinema.WebApi.Services.Interfaces
{
    public interface IMovieService
    {
        GetMovieResponse? Create(CreateMovieDTO movie);
        IEnumerable<GetMovieResponse> GetAll();
        UpdateMovieDTO? Update(int id, UpdateMovieDTO movie);
        void Delete(int id);
        IEnumerable<GetMovieResponse> GetByName(string name);
        GetMovieResponse? GetById(int id);
        Movie? AddGenre(int idMovie, int idGenre);
        Movie? RemoveGenre(int idMovie, int idGenre);
    }
}