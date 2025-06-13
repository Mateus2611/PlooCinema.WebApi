using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using PlooCinema.Core.DTOs;
using PlooCinema.Core.Models;
using PlooCinema.Core.Repositories;
using PlooCinema.Core.Responses;
using PlooCinema.Core.Services.Interfaces;

namespace PlooCinema.Core.Services
{
    public class GenreService(IGenreRepository genreRepository, IMapper mapper) : IGenreService
    {
        private readonly IGenreRepository genreRepository = genreRepository;
        private readonly IMapper mapper = mapper;

        public async Task<GenreResponse> CreateAsync(GenreDTO genreDTO)
        {
            var newGenre = mapper.Map<Genre>(genreDTO);

            return
                mapper.Map<GenreResponse>
                (
                    await genreRepository.CreateAsync(newGenre)
                );
        }

        public async Task<IEnumerable<GenreResponse>> GetAllAsync(int skip, int take)
        {
            return
                mapper.Map<IEnumerable<GenreResponse>>
                (
                    await genreRepository.GetAllAsync(skip, take)
                );
        }

        public async Task<GenreResponse> UpdateAsync(Guid id, GenreDTO genreDTO)
        {
            var genreUpdated = mapper.Map<Genre>(genreDTO);
            genreUpdated.Id = id;

            return
                mapper.Map<GenreResponse>
                (
                    await genreRepository.UpdateAsync(genreUpdated)
                );
        }

        public async Task DeleteAsync(Guid id)
        {
            var genreDeleted = await genreRepository.GetByIdAsync(id) ?? throw new Exception("Filme não encontrado.");
            await genreRepository.DeleteAsync(genreDeleted);
        }

        public async Task<GenreResponse> GetByIdAsync(Guid id)
        {
            return
                mapper.Map<GenreResponse>
                (
                    await genreRepository.GetByIdAsync(id)
                );
        }

        public async Task<IEnumerable<GenreResponse>> GetByNameAsync(string name, int skip, int take)
        {
            return 
                mapper.Map<IEnumerable<GenreResponse>>
                (
                    await genreRepository.GetByNameAsync(name, skip, take)
                );
        }
    }
}