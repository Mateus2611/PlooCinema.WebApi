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
        IEnumerable<Movie> SearchAll();
        IEnumerable<Movie> SearchByName(string name);
        Movie? SearchById(int id);
        Movie? Update(int id, Movie movie);
        void Delete(int id);
    }
}