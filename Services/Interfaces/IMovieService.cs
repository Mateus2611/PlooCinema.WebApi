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
        UpdateMovieDTO? Update(Guid id, UpdateMovieDTO movie);
        void Delete(Guid id);
        IEnumerable<GetMovieResponse> GetByName(string name);
        GetMovieResponse? GetById(Guid id);
        GetMovieResponse? AddGenre(MovieGenreDTO movieGenreIds);
        GetMovieResponse? RemoveGenre(MovieGenreDTO movieGenreids);
    }
}