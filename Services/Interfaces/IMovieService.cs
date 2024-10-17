using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PlooCinema.WebApi.Model;
using PlooCinema.WebApi.Models.DTOs;
using PlooCinema.WebApi.Models.Responses;

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
        GetMovieResponse? AddGenre(MovieGenreDTO movieGenreIds);
        GetMovieResponse? RemoveGenre(MovieGenreDTO movieGenreids);
    }
}