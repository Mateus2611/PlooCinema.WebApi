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

            Movie? movieConverted = mapper.Map<Movie>(movie);

            movieConverted = movieRepository.Create(movieConverted);

            if (movieConverted is null)
                return null;

            MovieGenreDTO movieGenreDTO = new(movie.IdsGenres, movieConverted.Id);

            return AddGenre(movieGenreDTO);
        }

        public UpdateMovieDTO? Update(Guid id, UpdateMovieDTO movie)
        {
            var updatedValue = mapper.Map<Movie>(movie);
            updatedValue.Id = id;

            return
                mapper.Map<UpdateMovieDTO>
                (
                    movieRepository.Update(updatedValue)
                );
        }

        public void Delete(Guid id)
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

        public GetMovieResponse? GetById(Guid id)
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

            foreach (Guid genre in movieGenreIds.GenresIds)
            {
                try
                {
                    var getGenre = genreRepository.GetById(genre);

                    if (getGenre is not null)
                        getMovie.Genres.Add(getGenre);
                }
                catch {}
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

            foreach (Guid genre in movieGenreIds.GenresIds)
            {
                try
                {
                    var getGenre = genreRepository.GetById(genre);

                    if (getGenre is not null)
                        getMovie.Genres.Remove(getGenre);
                }
                catch {}
            }

            return
                mapper.Map<GetMovieResponse>
                (
                    movieRepository.Update(getMovie)
                );
        }
    }
}