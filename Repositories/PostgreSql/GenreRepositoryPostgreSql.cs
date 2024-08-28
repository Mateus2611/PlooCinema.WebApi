using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Npgsql;
using PlooCinema.WebApi.Model;
using PlooCinema.WebApi.Models;

namespace PlooCinema.WebApi.Repositories.PostgreSql
{
    public class GenreRepositoryPostgreSql : IGenreRepository
    {
        public GenreRepositoryPostgreSql(string connString)
        {
            Conn = new NpgsqlConnection(connString);
        }

        private NpgsqlConnection Conn { get; set; }

        public Genre? Create(string name, IEnumerable<int> idMovies)
        {
            IMovieRepository searchMovies = new MovieRepositoryPostgreSql(Conn.ToString());

            IEnumerable<Movie> movies = [];

            Conn.Open();

            foreach (int id in idMovies)
            {
                try
                {
                    movies.Append(searchMovies.SearchById(id));
                }
                catch
                {
                    continue;
                }
            }

            var command = new NpgsqlCommand("INSERT INTO genre (name) VALUES @name RETURNING id, name", Conn);

            command.Parameters.AddWithValue("name", name);

            var reader = command.ExecuteReader();

            if (!reader.HasRows && reader.Read())
            {
                
            }
        }

        public void AddMovies(IEnumerable<int> idsMovies, int idGenre)
        {
            foreach (int idMovie in idsMovies)
            {
                try
                {
                    Conn.Open();

                    var command = new NpgsqlCommand("INSERT INTO movie_genre (movie_id, genre_id) VALUES (@id_movie, @id_genre) RETURNING movie_id, genre_id", Conn);

                    command.Parameters.AddWithValue("movie_id", idMovie);
                    command.Parameters.AddWithValue("genre_id", idGenre);

                    command.ExecuteNonQuery();

                    Conn.Close();
                } catch
                {
                    continue;
                }
            }
        }

        public Genre? SearchById(int id)
        {
            Conn.Open();

            var command = new NpgsqlCommand("SELECT * FROM genre WHERE id = @id ", Conn);

            command.Parameters.AddWithValue("id", id);

            var reader = command.ExecuteReader();

            if (!reader.HasRows && reader.Read())
            {
                Genre genre = new(reader.GetInt32(reader.GetOrdinal("id")), reader.GetString(reader.GetOrdinal("name")));

                Conn.Close();

                return genre;
            }

            Conn.Close();
            return null;
        }
    }
}