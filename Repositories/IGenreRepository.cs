using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PlooCinema.WebApi.Model;
using PlooCinema.WebApi.Models;

namespace PlooCinema.WebApi.Repositories
{
    public interface IGenreRepository
    {
        Task<Genre> CreateAsync(Genre genre);
        Task<IEnumerable<Genre>> GetAllAsync();
        Task<Genre> UpdateAsync(Genre genre);
        Task DeleteAsync(Genre genre);
        Task<IEnumerable<Genre>> GetByNameAsync(string name);
        Task<Genre?> GetByIdAsync(Guid id);
    }
}