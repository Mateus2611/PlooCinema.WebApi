using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using PlooCinema.Core.Models;

namespace PlooCinema.Core.Repositories
{
    public interface IMovieRepository
    {
        Task<Movie> CreateAsync(Movie movie);
        Task<IEnumerable<Movie>> GetAllAsync(int skip, int take);
        Task<Movie> UpdateAsync(Movie movie);
        Task DeleteAsync(Movie movie);
        Task<IEnumerable<Movie>> GetByNameAsync(string name, int skip, int take);
        Task<Movie?> GetByIdAsync(Guid id);
    }
}