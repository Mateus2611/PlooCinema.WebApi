using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using PlooCinema.WebApi.Models;
using PlooCinema.WebApi.Models.DTOs;
using PlooCinema.WebApi.Models.Responses;
using PlooCinema.WebApi.Repositories;
using PlooCinema.WebApi.Services.Interfaces;

namespace PlooCinema.WebApi.Services
{
    public class GenreService(IGenreRepository genreRepository, IMapper mapper) : IGenreService
    {
        private readonly IGenreRepository genreRepository = genreRepository;
        private readonly IMapper mapper = mapper;

        public GenreResponse? Create(GenreDTO genreDTO)
        {
            var newGenre = mapper.Map<Genre>(genreDTO);

            return
                mapper.Map<GenreResponse>
                (
                    genreRepository.Create(newGenre)
                );
        }

        public IEnumerable<GenreResponse> GetAll()
        {
            return
                mapper.Map<IEnumerable<GenreResponse>>
                (
                    genreRepository.GetAll()
                );
        }

        public GenreResponse? Update(int id, GenreDTO genreDTO)
        {
            var genreUpdated = mapper.Map<Genre>(genreDTO);
            genreUpdated.Id = id;

            return
                mapper.Map<GenreResponse>
                (
                    genreRepository.Update(genreUpdated)
                );
        }

        public void Delete(int id)
        {
            var genreDeleted = genreRepository.GetById(id) ?? throw new Exception("Filme não encontrado.");
            genreRepository.Delete(genreDeleted);
        }

        public GenreResponse? GetById(int id)
        {
            return
                mapper.Map<GenreResponse>
                (
                    genreRepository.GetById(id)
                );
        }

        public IEnumerable<GenreResponse> GetByName(string name)
        {
            return 
                mapper.Map<IEnumerable<GenreResponse>>
                (
                    genreRepository.GetByName(name)
                );
        }
    }
}