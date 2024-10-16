using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PlooCinema.WebApi.Models;
using PlooCinema.WebApi.Models.DTOs;

namespace PlooCinema.WebApi.Services.Interfaces
{
    public interface IGenreService
    {
        GenreDTO? Create(GenreDTO genreDTO);
        IEnumerable<GenreDTO> GetAll();
        GenreDTO? Update(GenreDTO genreDTO);
        void Delete(GenreDTO genreDTO);
        IEnumerable<GenreDTO> GetByName(string name);
        GenreDTO? GetById(int id);
    }
}