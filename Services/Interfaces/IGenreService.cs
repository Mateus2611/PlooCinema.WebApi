using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PlooCinema.WebApi.Models;

namespace PlooCinema.WebApi.Services.Interfaces
{
    public interface IGenreService
    {
        Genre? Create(Genre genre);
        IEnumerable<Genre> GetAll();
        Genre? Update(Genre genre);
        void Delete(Genre genre);
        IEnumerable<Genre> GetByName(string name);
        Genre? GetById(int id);
    }
}