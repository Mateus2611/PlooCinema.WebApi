// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using PlooCinema.WebApi.Repositories;
// using PlooCinema.WebApi.Model;
// using System.Text.Json;

// namespace PlooCinema.WebApi.Repositories.Json
// {
//     public class MovieRepositoryJson : IMovieRepository
//     {
//         public MovieRepositoryJson()
//         {
//             var infoFile = new FileInfo(fileMovie);
//             if (!infoFile.Exists)
//             {
//                 File.Create(fileMovie).Close();
//                 File.WriteAllText(fileMovie, "[]");
//             }
//         }

//         string fileMovie = "DbMovie.json";

//         public Movie? Create(Movie movie)
//         {
//             var getAllMovies = File.ReadAllText(fileMovie);
//             var jsonMovie = JsonSerializer.Deserialize<IEnumerable<Movie>>(getAllMovies) ?? [];

//             movie.Id = jsonMovie.Count() + 1;

//             var newJsonList = jsonMovie.Append(movie);
//             var listToJson = JsonSerializer.Serialize<IEnumerable<Movie>>(newJsonList);
//             File.WriteAllText(fileMovie, listToJson);

//             return movie;
//         }

//         public IEnumerable<Movie> SearchAll()
//         {
//             var getJsonMovie = File.ReadAllText(fileMovie);
//             var jsonMovie = JsonSerializer.Deserialize<IEnumerable<Movie>>(getJsonMovie) ?? [];

//             return jsonMovie;
//         }

//         public IEnumerable<Movie> SearchByName(string name)
//         {
//             var getJsonMovie = File.ReadAllText(fileMovie);
//             var jsonMovie = JsonSerializer.Deserialize<IEnumerable<Movie>>(getJsonMovie) ?? [];

//             var query = jsonMovie
//                 .Where(movie => movie.Name.Contains(name, StringComparison.CurrentCultureIgnoreCase));

//             return query;
//         }

//         public Movie? SearchById(int id)
//         {
//             var getJsonMovie = File.ReadAllText(fileMovie);
//             var jsonMovie = JsonSerializer.Deserialize<IEnumerable<Movie>>(getJsonMovie) ?? [];

//             var query = jsonMovie
//                 .SingleOrDefault(movie => movie.Id == id);

//             return query;
//         }

//         public Movie? Update(int id, Movie movie)
//         {
//             var getJsonMovie = File.ReadAllText(fileMovie);
//             var jsonMovie = JsonSerializer.Deserialize<IEnumerable<Movie>>(getJsonMovie) ?? [];

//             var query = jsonMovie
//                 .Single(movie => movie.Id == id);

//             if (query != null)
//             {
//                 query.Name = movie.Name;
//                 query.Genres = movie.Genres;
//                 query.Duration = movie.Duration;
//                 query.Release = movie.Release;
//                 query.Description = movie.Description;
//             }

//             var newJson = JsonSerializer.Serialize(jsonMovie);
//             File.WriteAllText(fileMovie, newJson);

//             return query;
//         }

//         public void Delete(int id)
//         {
//             var getJsonMovie = File.ReadAllText(fileMovie);
//             var jsonMovie = JsonSerializer.Deserialize<List<Movie>>(getJsonMovie) ?? [];

//             var query = jsonMovie
//                 .Single(movie => movie.Id == id);

//             if (query != null)
//                 jsonMovie.Remove(query);

//             var newJson = JsonSerializer.Serialize(jsonMovie);
//             File.WriteAllText(fileMovie, newJson);
//         }
//     }
// }