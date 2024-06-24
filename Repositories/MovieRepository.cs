using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.Data;
using PlooCinema.WebApi.Model;

namespace PlooCinema.WebApi.Repositories
{
    
    public interface IMovieRepository
    {
        void Create(Movie movie);
        IEnumerable<Movie> SearchAll();
        // IEnumerable<Movie> SearchByName(string name);
        // IEnumerable<Movie> SearchById(int id);
        // void Update(int id, Movie movie);
        // void Delete(int id);
    }

    public class MovieRepositoryJson : IMovieRepository
    {
        public MovieRepositoryJson()
        {
            var infoFile = new FileInfo(fileMovie);
            if (!infoFile.Exists)
            {
                File.Create(fileMovie).Close();
                File.WriteAllText(fileMovie, "[]");
            }
        }

        string fileMovie = "DbMovie.json";

        public void Create(Movie movie)
        {
            var getAllMovies = File.ReadAllText(fileMovie);
            var jsonMovie = JsonSerializer.Deserialize<IEnumerable<Movie>>(getAllMovies) ?? [];

            movie.Id = jsonMovie.Count() + 1;

            var newJsonList = jsonMovie.Append(movie);
            var listToJson = JsonSerializer.Serialize<IEnumerable<Movie>>(newJsonList);
            File.WriteAllText(fileMovie, listToJson);
        }

        public IEnumerable<Movie> SearchAll()
        {
            var getJsonMovie = File.ReadAllText(fileMovie);
            var jsonMovie = JsonSerializer.Deserialize<IEnumerable<Movie>>(getJsonMovie) ?? [];

            return jsonMovie;
        }
    }
}