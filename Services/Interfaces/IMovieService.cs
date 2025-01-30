using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PlooCinema.WebApi.Model;
using PlooCinema.WebApi.Models.DTOs;
using PlooCinema.WebApi.Models.Responses;

namespace PlooCinema.WebApi.Services.Interfaces
{
    public interface IMovieService
    {
        Task<GetMovieResponse?> CreateAsync(CreateMovieDTO movie);
        Task<IEnumerable<GetMovieResponse>> GetAllAsync();
        Task<UpdateMovieDTO> UpdateAsync(Guid id, UpdateMovieDTO movie);
        Task DeleteAsync(Guid id);
        Task<IEnumerable<GetMovieResponse>> GetByNameAsync(string name);
        Task<GetMovieResponse> GetByIdAsync(Guid id);
        Task<GetMovieResponse?> AddGenreAsync(MovieGenreDTO movieGenreIds);
        Task<GetMovieResponse?> RemoveGenreAsync(MovieGenreDTO movieGenreids);
    }
}