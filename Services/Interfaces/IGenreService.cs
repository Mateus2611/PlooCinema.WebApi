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
        Task<GenreResponse> CreateAsync(GenreDTO genreDTO);
        Task<IEnumerable<GenreResponse>> GetAllAsync(int skip, int take);
        Task<GenreResponse> UpdateAsync(Guid id, GenreDTO genreDTO);
        Task DeleteAsync(Guid id);
        Task<IEnumerable<GenreResponse>> GetByNameAsync(string name, int skip, int take);
        Task<GenreResponse> GetByIdAsync(Guid id);
    }
}