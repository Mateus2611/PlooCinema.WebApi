using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using PlooCinema.Core.Models;

namespace PlooCinema.Core.Repositories
{
    public interface IGenreRepository
    {
        Task<Genre> CreateAsync(Genre genre);
        Task<IEnumerable<Genre>> GetAllAsync(int skip, int take);
        Task<Genre> UpdateAsync(Genre genre);
        Task DeleteAsync(Genre genre);
        Task<IEnumerable<Genre>> GetByNameAsync(string name, int skip, int take);
        Task<Genre?> GetByIdAsync(Guid id);
    }
}