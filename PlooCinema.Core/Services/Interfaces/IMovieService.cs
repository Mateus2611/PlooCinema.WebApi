using PlooCinema.Core.DTOs;
using PlooCinema.Core.Responses;

namespace PlooCinema.Core.Services.Interfaces
{
    public interface IMovieService
    {
        Task<GetMovieResponse?> CreateAsync(CreateMovieDTO movie);
        Task<IEnumerable<GetMovieResponse>> GetAllAsync(int skip, int take);
        Task<UpdateMovieDTO> UpdateAsync(Guid id, UpdateMovieDTO movie);
        Task DeleteAsync(Guid id);
        Task<IEnumerable<GetMovieResponse>> GetByNameAsync(string name, int skip, int take);
        Task<GetMovieResponse> GetByIdAsync(Guid id);
        Task<GetMovieResponse?> AddGenreAsync(MovieGenreDTO movieGenreIds);
        Task<GetMovieResponse?> RemoveGenreAsync(MovieGenreDTO movieGenreids);
    }
}