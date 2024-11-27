using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.Data;
using Npgsql;
using PlooCinema.WebApi.Model;

namespace PlooCinema.WebApi.Repositories
{
    public interface IMovieRepository
    {
        Movie? Create(Movie movie);
        IEnumerable<Movie> GetAll();
        Movie? Update(Movie movie);
        void Delete(Movie movie);
        IEnumerable<Movie> GetByName(string name);
        Movie? GetById(Guid id);
    }
}