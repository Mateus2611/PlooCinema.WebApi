// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using PlooCinema.WebApi.Model;
// using PlooCinema.WebApi.Repositories;
// using PlooCinema.WebApi.Services.Interfaces;

// namespace PlooCinema.WebApi.Services
// {
//     public class MovieServices( IMovieRepository movieRepository, IGenreRepository genreRepository) : IMovieService
//     {
//         private readonly IMovieRepository movieRepository = movieRepository;
//         private readonly IGenreRepository genreRepository = genreRepository;


//         public Movie? Create(Movie movie)
//         {
//             return movieRepository.Create(movie);
//         }
                     
//         public Movie? Update(Movie movie)
//         {
//             return movieRepository.Update(movie);
//         }

//         public void Delete(Movie movie)
//         {
//             movieRepository.Delete(movie);
//         }

//         public IEnumerable<Movie> GetAll()
//         {
//             return movieRepository.GetAll();
//         }

//         public Movie? GetById(int id)
//         {
//             return movieRepository.GetById(id);
//         }

//         public IEnumerable<Movie> GetByName(string name)
//         {
//             return movieRepository.GetByName(name);
//         }

        
//         public Movie? AddGenre(int idMovie, int idGenre)
//         {
//             var getMovie = movieRepository.GetById(idMovie);
//             var getGenre = genreRepository.GetById(idGenre);

//             if (getMovie is null || getGenre is null)
//                 return null;

//             getMovie.Genres.Add(getGenre);
//             return movieRepository.Update(getMovie);
//         }

//         public Movie? RemoveGenre(int idMovie, int idGenre)
//         {
//             var getMovie = movieRepository.GetById(idMovie);
//             var getGenre = genreRepository.GetById(idGenre);

//             if (getMovie is null || getGenre is null)
//                 return null;

//             getMovie.Genres.Remove(getGenre);
//             return movieRepository.Update(getMovie);
//         }
//     }
// }