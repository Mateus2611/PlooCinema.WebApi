using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using PlooCinema.WebApi.Model;
using PlooCinema.WebApi.Models.DTOs;
using PlooCinema.WebApi.Repositories;
using PlooCinema.WebApi.Services.Interfaces;

namespace PlooCinema.WebApi.Services
{
    public class MovieServices(IMovieRepository movieRepository, IGenreRepository genreRepository, IMapper mapper) : IMovieService
    {
        private readonly IMovieRepository movieRepository = movieRepository;
        private readonly IGenreRepository genreRepository = genreRepository;
        private readonly IMapper mapper = mapper;

        public GetMovieResponse? Create(CreateMovieDTO movie)
        {

            var movieConverted = mapper.Map<Movie>(movie);

            movieConverted = movieRepository.Create(movieConverted);

            if (movie.IdsGenres is not null && movie.IdsGenres.Any() && movieConverted is not null)
            {
                foreach (int idGenre in movie.IdsGenres)
                {
                    AddGenre(movieConverted.Id, idGenre);
                }
            }

            return
                mapper.Map<GetMovieResponse>(movieConverted);
        }

        public UpdateMovieDTO? Update(int id, UpdateMovieDTO movie)
        {
            var updatedValue = mapper.Map<Movie>(movie);
            updatedValue.Id = id;

            return 
                mapper.Map<UpdateMovieDTO>
                (
                    movieRepository.Update(updatedValue)
                );
        }

        public void Delete(int id)
        {
            var movieDelete = movieRepository.GetById(id) ?? throw new Exception("Filme nãoi encontrado.");
            movieRepository.Delete(movieDelete);
        }

        public IEnumerable<GetMovieResponse> GetAll()
        {
            return
                mapper.Map<IEnumerable<GetMovieResponse>>
                (
                    movieRepository.GetAll()
                );
        }

        public GetMovieResponse? GetById(int id)
        {
            return
                mapper.Map<GetMovieResponse>
                (
                    movieRepository.GetById(id)
                );

        }

        public IEnumerable<GetMovieResponse> GetByName(string name)
        {
            return
                mapper.Map<IEnumerable<GetMovieResponse>>
                (
                    movieRepository.GetByName(name)
                );
        }


        public Movie? AddGenre(int idMovie, int idGenre)
        {
            var getMovie = movieRepository.GetById(idMovie);
            var getGenre = genreRepository.GetById(idGenre);

            if (getMovie is null || getGenre is null)
                return null;

            getMovie.Genres.Add(getGenre);
            return movieRepository.Update(getMovie);
        }

        public Movie? RemoveGenre(int idMovie, int idGenre)
        {
            var getMovie = movieRepository.GetById(idMovie);
            var getGenre = genreRepository.GetById(idGenre);

            if (getMovie is null || getGenre is null)
                return null;

            getMovie.Genres.Remove(getGenre);
            return movieRepository.Update(getMovie);
        }
    }
}