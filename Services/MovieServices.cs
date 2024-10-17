using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using PlooCinema.WebApi.Controllers;
using PlooCinema.WebApi.Model;
using PlooCinema.WebApi.Models.DTOs;
using PlooCinema.WebApi.Models.Responses;
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
                MovieGenreDTO movieGenreDTO = new()
                {
                    GenresIds = movie.IdsGenres,
                    MovieId = movieConverted.Id
                };

                AddGenre(movieGenreDTO);
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
            var movieDelete = movieRepository.GetById(id) ?? throw new KeyNotFoundException(id.ToString());
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


        public GetMovieResponse? AddGenre(MovieGenreDTO movieGenreIds)
        {
            var getMovie = movieRepository.GetById(movieGenreIds.MovieId);

            if (getMovie is null)
                return null;

            foreach (int genre in movieGenreIds.GenresIds)
            {
                var getGenre = genreRepository.GetById(genre);

                if (getGenre is not null && !getMovie.Genres.Contains(getGenre))
                    getMovie.Genres.Add(getGenre);
            }

            return 
                mapper.Map<GetMovieResponse>
                (
                    movieRepository.Update(getMovie)
                );
        }

        public GetMovieResponse? RemoveGenre(MovieGenreDTO movieGenreIds)
        {
            var getMovie = movieRepository.GetById(movieGenreIds.MovieId);

            if (getMovie is null)
                return null;

            foreach (int genre in movieGenreIds.GenresIds)
            {
                var getGenre = genreRepository.GetById(genre);

                if (getGenre is not null)
                    getMovie.Genres.Remove(getGenre);
            }

            return 
                mapper.Map<GetMovieResponse>
                (
                    movieRepository.Update(getMovie)
                );
        }
    }
}