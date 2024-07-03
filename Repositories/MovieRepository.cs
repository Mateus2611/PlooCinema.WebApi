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
        Movie? Create(Movie movie);
        IEnumerable<Movie> SearchAll();
        IEnumerable<Movie> SearchByName(string name);
        Movie? SearchById(int id);
        Movie? Update(int id, Movie movie);
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

        public Movie? Create(Movie movie)
        {
            var getAllMovies = File.ReadAllText(fileMovie);
            var jsonMovie = JsonSerializer.Deserialize<IEnumerable<Movie>>(getAllMovies) ?? [];

            movie.Id = jsonMovie.Count() + 1;

            var newJsonList = jsonMovie.Append(movie);
            var listToJson = JsonSerializer.Serialize<IEnumerable<Movie>>(newJsonList);
            File.WriteAllText(fileMovie, listToJson);

            return movie;
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

        public Movie? SearchById(int id)
        {
            var getJsonMovie = File.ReadAllText(fileMovie);
            var jsonMovie = JsonSerializer.Deserialize<IEnumerable<Movie>>(getJsonMovie) ?? [];

            var query = jsonMovie
                .SingleOrDefault(movie => movie.Id == id);

            return query;
        }

        public Movie? Update(int id, Movie movie)
        {
            var getJsonMovie = File.ReadAllText(fileMovie);
            var jsonMovie = JsonSerializer.Deserialize<IEnumerable<Movie>>(getJsonMovie) ?? [];

            var query = jsonMovie
                .Single(movie => movie.Id == id);

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

            return query;
        }

        public void Delete(int id)
        {
            var getJsonMovie = File.ReadAllText(fileMovie);
            var jsonMovie = JsonSerializer.Deserialize<List<Movie>>(getJsonMovie) ?? [];

            var query = jsonMovie
                .Single(movie => movie.Id == id);

            if (query != null)
                jsonMovie.Remove(query);

            var newJson = JsonSerializer.Serialize(jsonMovie);
            File.WriteAllText(fileMovie, newJson);
        }
    }

    public class MovieRepositoryPostgresSql : IMovieRepository
    {
        public MovieRepositoryPostgresSql(string connString)
        {
            Conn = new NpgsqlConnection(connString);
        }

        private NpgsqlConnection Conn { get; set; }

        public Movie? Create(Movie movie)
        {
            Conn.Open();

            var command = new NpgsqlCommand("INSERT INTO movie (name, genre, description, duration_minutes, release) VALUES (@name, @genre, @description, @duration_minutes, @release) RETURNING id, name, genre, description, duration_minutes, release", Conn);

            command.Parameters.AddWithValue("name", movie.Name);
            command.Parameters.AddWithValue("genre", movie.Genre);
            command.Parameters.AddWithValue("description", movie.Description);
            command.Parameters.AddWithValue("duration_minutes", movie.Duration);
            command.Parameters.AddWithValue("release", movie.Release.Date);

            var reader = command.ExecuteReader();

            if (reader.HasRows && reader.Read())
            {
                Movie savedData = new(reader.GetInt32(reader.GetOrdinal("id")), reader.GetString(reader.GetOrdinal("name")), reader.GetString(reader.GetOrdinal("genre")), reader.GetInt32(reader.GetOrdinal("duration_minutes")), reader.GetDateTime(reader.GetOrdinal("release")), reader.GetString(reader.GetOrdinal("description")));

                Conn.Close();

                return savedData;
            }

            return null;
        }

        public IEnumerable<Movie> SearchAll()
        {
            List<Movie> movies = [];

            Conn.Open();

            var command = new NpgsqlCommand("SELECT * FROM movie", Conn);
            var reader = command.ExecuteReader();

            while (reader.HasRows && reader.Read())
            {
                var movie = new Movie(reader.GetInt32(reader.GetOrdinal("id")), reader.GetString(reader.GetOrdinal("name")), reader.GetString(reader.GetOrdinal("genre")), reader.GetInt32(reader.GetOrdinal("duration_minutes")), reader.GetDateTime(reader.GetOrdinal("release")), reader.GetString(reader.GetOrdinal("description")));

                movies.Add(movie);
            }

            Conn.Close();

            return movies.AsEnumerable<Movie>();
        }

        public IEnumerable<Movie> SearchByName(string name)
        {
            List<Movie> movies = [];

            Conn.Open();

            var command = new NpgsqlCommand("SELECT * FROM movie WHERE (name) ILIKE '%' || @name || '%'", Conn);

            command.Parameters.AddWithValue("name", name);

            var reader = command.ExecuteReader();

            while (reader.HasRows && !reader.Read())
            {
                var movie = new Movie(reader.GetInt32(reader.GetOrdinal("id")), reader.GetString(reader.GetOrdinal("name")), reader.GetString(reader.GetOrdinal("genre")), reader.GetInt32(reader.GetOrdinal("duration_minutes")), reader.GetDateTime(reader.GetOrdinal("release")), reader.GetString(reader.GetOrdinal("description")));

                movies.Add(movie);
            }

            Conn.Close();

            return movies.AsEnumerable<Movie>();
        }

        public Movie? SearchById(int id)
        {
            Conn.Open();

            var command = new NpgsqlCommand("SELECT * FROM movie WHERE id = @id", Conn);

            command.Parameters.AddWithValue("id", id);

            var reader = command.ExecuteReader();

            if ((reader.HasRows) && reader.Read())
            {
                Movie movie = new(reader.GetInt32(reader.GetOrdinal("id")), reader.GetString(reader.GetOrdinal("name")), reader.GetString(reader.GetOrdinal("genre")), reader.GetInt32(reader.GetOrdinal("duration_minutes")), reader.GetDateTime(reader.GetOrdinal("release")), reader.GetString(reader.GetOrdinal("description")));

                Conn.Close();

                return movie;
            }

            Conn.Close();
            return null;
        }

        public Movie? Update(int id, Movie movie)
        {
            Conn.Open();

            var command = new NpgsqlCommand("UPDATE movie SET name = @name, genre = @genre,  description = @description, duration_minutes = @duration_minutes, release = @release WHERE id = @id RETURNING id, name, genre, description, duration_minutes, release", Conn);

            command.Parameters.AddWithValue("id", id);
            command.Parameters.AddWithValue("name", movie.Name);
            command.Parameters.AddWithValue("genre", movie.Genre);
            command.Parameters.AddWithValue("description", movie.Description);
            command.Parameters.AddWithValue("duration_minutes", movie.Duration);
            command.Parameters.AddWithValue("release", movie.Release.Date);

            var reader = command.ExecuteReader();

            if (reader.HasRows && reader.Read())
            {
                Movie savedData = new(reader.GetInt32(reader.GetOrdinal("id")), reader.GetString(reader.GetOrdinal("name")), reader.GetString(reader.GetOrdinal("genre")), reader.GetInt32(reader.GetOrdinal("duration_minutes")), reader.GetDateTime(reader.GetOrdinal("release")), reader.GetString(reader.GetOrdinal("description")));

                Conn.Close();

                return savedData;
            }

            return null;
        }

        public void Delete(int id)
        {
            Conn.Open();

            var command = new NpgsqlCommand("DELETE FROM movie WHERE id = @id", Conn);

            command.Parameters.AddWithValue("id", id);

            command.ExecuteNonQuery();

            Conn.Close();
        }
    }
}