using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using PlooCinema.WebApi.Models;
using PlooCinema.WebApi.Models.DTOs;
using PlooCinema.WebApi.Repositories;
using PlooCinema.WebApi.Services.Interfaces;

namespace PlooCinema.WebApi.Services
{
    public class GenreService(IGenreRepository genreRepository, IMapper mapper) : IGenreService
    {
        private readonly IGenreRepository genreRepository = genreRepository;
        private readonly IMapper mapper = mapper;

        public GenreDTO? Create(GenreDTO genreDTO)
        {
            var newGenre = mapper.Map<Genre>(genreDTO);

            return
                mapper.Map<GenreDTO>
                (
                    genreRepository.Create(newGenre)
                );
        }

        public IEnumerable<GenreDTO> GetAll()
        {
            return
                mapper.Map<IEnumerable<GenreDTO>>
                (
                    genreRepository.GetAll()
                );
        }

        public GenreDTO? Update(GenreDTO genreDTO)
        {
            var genreUpdated = mapper.Map<Genre>(genreDTO);

            return
                mapper.Map<GenreDTO>
                (
                    genreRepository.Update(genreUpdated)
                );
        }

        public void Delete(GenreDTO genreDTO)
        {
            var genreDeleted = mapper.Map<Genre>(genreDTO);

            genreRepository.Delete(genreDeleted);
        }

        public GenreDTO? GetById(int id)
        {
            return
                mapper.Map<GenreDTO>
                (
                    genreRepository.GetById(id)
                );
        }

        public IEnumerable<GenreDTO> GetByName(string name)
        {
            return 
                mapper.Map<IEnumerable<GenreDTO>>
                (
                    genreRepository.GetByName(name)
                );
        }
    }
}