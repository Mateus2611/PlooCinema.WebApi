using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PlooCinema.WebApi.Models;
using PlooCinema.WebApi.Models.DTOs;
using PlooCinema.WebApi.Models.Responses;

namespace PlooCinema.WebApi.Services.Interfaces
{
    public interface IGenreService
    {
        GenreResponse? Create(GenreDTO genreDTO);
        IEnumerable<GenreResponse> GetAll();
        GenreResponse? Update(int id, GenreDTO genreDTO);
        void Delete(int id);
        IEnumerable<GenreResponse> GetByName(string name);
        GenreResponse? GetById(int id);
    }
}