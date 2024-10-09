using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PlooCinema.WebApi.Models;
using PlooCinema.WebApi.Repositories;
using PlooCinema.WebApi.Services.Interfaces;

namespace PlooCinema.WebApi.Services
{
    public class GenreService(IGenreRepository genreRepository) : IGenreService
    {
        private readonly IGenreRepository genreRepository = genreRepository;
        public Genre? Create(Genre genre)
        {
            return genreRepository.Create(genre);
        }

        public IEnumerable<Genre> GetAll()
        {
            return genreRepository.GetAll();
        }

        public Genre? Update(Genre genre)
        {
            return genreRepository.Update(genre);
        }

        public void Delete(Genre genre)
        {
            genreRepository.Delete(genre);
        }

        public Genre? GetById(int id)
        {
            return genreRepository.GetById(id);
        }

        public IEnumerable<Genre> GetByName(string name)
        {
            return genreRepository.GetByName(name);
        }
    }
}