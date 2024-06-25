using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.Data;
using Npgsql;
using PlooCinema.WebApi.Model;

namespace PlooCinema.WebApi.Repositories
{

    public interface IMovieRepository
    {
        void Create(Movie movie);
        IEnumerable<Movie> SearchAll();
        IEnumerable<Movie> SearchByName(string name);
        IEnumerable<Movie> SearchById(int id);
        void Update(int id, Movie movie);
        void Delete(int id);
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

        public IEnumerable<Movie> SearchByName(string name)
        {
            var getJsonMovie = File.ReadAllText(fileMovie);
            var jsonMovie = JsonSerializer.Deserialize<IEnumerable<Movie>>(getJsonMovie) ?? [];

            var query = jsonMovie
                .Where(movie => movie.Name.Contains(name, StringComparison.CurrentCultureIgnoreCase));

            return query;
        }

        public IEnumerable<Movie> SearchById(int id)
        {
            var getJsonMovie = File.ReadAllText(fileMovie);
            var jsonMovie = JsonSerializer.Deserialize<IEnumerable<Movie>>(getJsonMovie) ?? [];

            var query = jsonMovie
                .Where(movie => movie.Id == id);

            return query;
        }

        public void Update(int id, Movie movie)
        {
            var getJsonMovie = File.ReadAllText(fileMovie);
            var jsonMovie = JsonSerializer.Deserialize<IEnumerable<Movie>>(getJsonMovie) ?? [];

            var query = jsonMovie
                .SingleOrDefault(movie => movie.Id == id);

            if (query != null)
            {
                query.Name = movie.Name;
                query.Genre = movie.Genre;
                query.Duration = movie.Duration;
                query.Release = movie.Release;
                query.Description = movie.Description;
            }

            var newJson = JsonSerializer.Serialize(jsonMovie);
            File.WriteAllText(fileMovie, newJson);
        }

        public void Delete(int id)
        {
            var getJsonMovie = File.ReadAllText(fileMovie);
            var jsonMovie = JsonSerializer.Deserialize<List<Movie>>(getJsonMovie) ?? [];

            var query = jsonMovie
                .SingleOrDefault(movie => movie.Id == id);

            if (query != null)
                jsonMovie.Remove(query);

            var newJson = JsonSerializer.Serialize(jsonMovie);
            File.WriteAllText(fileMovie, newJson);
        }
    }

    public class MovieRepositoryPostgresSql : IMovieRepository
    {
        public MovieRepositoryPostgresSql()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            var connString = config.GetConnectionString("DefaultConnection");

            connString = connString.Replace("${HOST_POSTGRES}", config["AppSettings:HOST_POSTGRES"]);
            connString = connString.Replace("${PORT_POSTGRES}", config["AppSettings:PORT_POSTGRES"]);
            connString = connString.Replace("${USERNAME_POSTGRES}", config["AppSettings:USERNAME_POSTGRES"]);
            connString = connString.Replace("${PASSWORD_POSTGRES}", config["AppSettings:PASSWORD_POSTGRES"]);
            connString = connString.Replace("${DATA_POSTGRES}", config["AppSettings:DATA_POSTGRES"]);

            _conn = new NpgsqlConnection(connString);
        }

        private NpgsqlConnection _conn { get; set; }

        public void Create(Movie movie)
        {
            _conn.Open();

            var cmd = new NpgsqlCommand("INSERT INTO movie (name, genre, description, duration_minutes, release) VALUES (@name, @genre, @description, @duration_minutes, @release)", _conn);

            cmd.Parameters.AddWithValue("name", movie.Name);
            cmd.Parameters.AddWithValue("genre", movie.Genre);
            cmd.Parameters.AddWithValue("description", movie.Description);
            cmd.Parameters.AddWithValue("duration_minutes", movie.Duration);
            cmd.Parameters.AddWithValue("release", movie.Release);

            cmd.ExecuteNonQuery();

            _conn.Close();
        }

        public IEnumerable<Movie> SearchAll()
        {
            List<Movie> moviesList = [];

            _conn.Open();

            var command = new NpgsqlCommand("SELECT * FROM movie", _conn);
            var reader = command.ExecuteReader();

            while (reader.HasRows && reader.Read())
            {
                var movie = new Movie(reader.GetInt32(reader.GetOrdinal("id")), reader.GetString(reader.GetOrdinal("name")), reader.GetString(reader.GetOrdinal("genre")), reader.GetInt32(reader.GetOrdinal("duration_minutes")), reader.GetDateTime(reader.GetOrdinal("release")), reader.GetString(reader.GetOrdinal("description")));

                moviesList.Add(movie);
            }

            _conn.Close();

            return moviesList.AsEnumerable();
        }

        public IEnumerable<Movie> SearchByName(string name)
        {
            List<Movie> moviesList = [];

            _conn.Open();

            var command = new NpgsqlCommand("SELECT * FROM movie WHERE (name) ILIKE '%' || @name || '%'", _conn);

            command.Parameters.AddWithValue("name", name);

            var reader = command.ExecuteReader();

            while (reader.HasRows && reader.Read())
            {
                var movie = new Movie(reader.GetInt32(reader.GetOrdinal("id")), reader.GetString(reader.GetOrdinal("name")), reader.GetString(reader.GetOrdinal("genre")), reader.GetInt32(reader.GetOrdinal("duration_minutes")), reader.GetDateTime(reader.GetOrdinal("release")), reader.GetString(reader.GetOrdinal("description")));

                moviesList.Add(movie);
            }

            _conn.Close();

            return moviesList.AsEnumerable();
        }

        public IEnumerable<Movie> SearchById(int id)
        {
            List<Movie> moviesList = [];

            _conn.Open();

            var command = new NpgsqlCommand("SELECT FROM movie WHERE id = @id", _conn);
            
            command.Parameters.AddWithValue("id", id);

            var reader = command.ExecuteReader();

            while (reader.HasRows && reader.Read())
            {
                var movie = new Movie(reader.GetInt32(reader.GetOrdinal("id")), reader.GetString(reader.GetOrdinal("name")), reader.GetString(reader.GetOrdinal("genre")), reader.GetInt32(reader.GetOrdinal("duration_minutes")), reader.GetDateTime(reader.GetOrdinal("release")), reader.GetString(reader.GetOrdinal("description")));

                moviesList.Add(movie);
            }

            return moviesList.AsEnumerable();
        }

        public void Update(int id, Movie movie)
        {
            
        }

        public void Delete(int id)
        {

        }
    }
}