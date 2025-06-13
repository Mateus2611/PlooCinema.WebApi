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
    public class MovieServices(IMovieRepository movieRepository, IGenreRepository genreRepository, IMapper mapper) : IMovieService
    {
        private readonly IMovieRepository movieRepository = movieRepository;
        private readonly IGenreRepository genreRepository = genreRepository;
        private readonly IMapper mapper = mapper;

        public async Task<GetMovieResponse?> CreateAsync(CreateMovieDTO movie)
        {

            Movie? movieConverted = mapper.Map<Movie>(movie);

            movieConverted = await movieRepository.CreateAsync(movieConverted);

            MovieGenreDTO movieGenreDTO = new(movie.IdsGenres, movieConverted.Id);

            return await AddGenreAsync(movieGenreDTO);
        }

        public async Task<UpdateMovieDTO> UpdateAsync(Guid id, UpdateMovieDTO movie)
        {
            var updatedValue = mapper.Map<Movie>(movie);
            updatedValue.Id = id;

            return
                mapper.Map<UpdateMovieDTO>
                (
                    await movieRepository.UpdateAsync(updatedValue)
                );
        }

        public async Task DeleteAsync(Guid id)
        {
            var movieDelete = await movieRepository.GetByIdAsync(id) ?? throw new KeyNotFoundException(id.ToString());
            await movieRepository.DeleteAsync(movieDelete);
        }

        public async Task<IEnumerable<GetMovieResponse>> GetAllAsync(int skip, int take)
        {
            return
                mapper.Map<IEnumerable<GetMovieResponse>>
                (
                    await movieRepository.GetAllAsync(skip, take)
                );
        }

        public async Task<GetMovieResponse> GetByIdAsync(Guid id)
        {
            return
                mapper.Map<GetMovieResponse>
                (
                    await movieRepository.GetByIdAsync(id)
                );

        }

        public async Task<IEnumerable<GetMovieResponse>> GetByNameAsync(string name, int skip, int take)
        {
            return
                mapper.Map<IEnumerable<GetMovieResponse>>
                (
                    await movieRepository.GetByNameAsync(name, skip, take)
                );
        }


        public async Task<GetMovieResponse?> AddGenreAsync(MovieGenreDTO movieGenreIds)
        {
            var getMovie = await movieRepository.GetByIdAsync(movieGenreIds.MovieId);

            if (getMovie is null)
                return null;

            foreach (Guid genre in movieGenreIds.GenresIds)
            {
                try
                {
                    var getGenre = await genreRepository.GetByIdAsync(genre);

                    if (getGenre is not null && !getMovie.Genres.Contains(getGenre))
                    {
                        getMovie.Genres.Add(getGenre);
                        await movieRepository.UpdateAsync(getMovie);
                    }
                }
                catch { }
            }

            return
                mapper.Map<GetMovieResponse>
                (
                    getMovie
                );
        }

        public async Task<GetMovieResponse?> RemoveGenreAsync(MovieGenreDTO movieGenreIds)
        {
            var getMovie = await movieRepository.GetByIdAsync(movieGenreIds.MovieId);

            if (getMovie is null)
                return null;

            foreach (Guid genre in movieGenreIds.GenresIds)
            {
                try
                {
                    var getGenre = getMovie.Genres.FirstOrDefault( g => g.Id.Equals(genre) );

                    if (getGenre is not null)
                    {
                        getMovie.Genres.Remove(getGenre);
                    }
                }
                catch { }
            }

            await movieRepository.UpdateAsync(getMovie);

            return
                mapper.Map<GetMovieResponse>
                (
                    getMovie
                );
        }
    }
}