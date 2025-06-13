using PlooCinema.Core.DTOs;
using PlooCinema.Core.Responses;

namespace PlooCinema.Core.Services.Interfaces
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