using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PlooCinema.WebApi.Models;

namespace PlooCinema.WebApi.Repositories
{
    public interface GenreRepository
    {
        Genre? Create (Genre genre);
        IEnumerable<Genre> SearchAll();
        IEnumerable<Genre> SearchByName(string name);
        Genre? SearchById(int id);
        Genre? Update(int id, Genre genre);
        void Delete(int id);
    }
}