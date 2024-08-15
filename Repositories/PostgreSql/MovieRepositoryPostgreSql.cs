using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlooCinema.WebApi.Repositories.PostgreSql
{
    using PlooCinema.WebApi.Model;
    using PlooCinema.WebApi.Repositories;
    using Npgsql;

    public class MovieRepositoryPostgreSql : IMovieRepository
    {
        public MovieRepositoryPostgreSql(string connString)
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

            while (reader.HasRows && reader.Read())
            {
                var movie = new Movie(reader.GetInt32(reader.GetOrdinal("id")), reader.GetString(reader.GetOrdinal("name")), reader.GetString(reader.GetOrdinal("genre")), reader.GetInt32(reader.GetOrdinal("duration_minutes")), reader.GetDateTime(reader.GetOrdinal("release")), reader.GetString(reader.GetOrdinal("description")));

                movies.Add(movie);
            }

            Conn.Close();

            return movies.AsEnumerable();
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