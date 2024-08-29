using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.Features;
using Npgsql;
using Npgsql.Internal;
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
            Conn.Open();
            
            var command = new NpgsqlCommand("INSERT INTO genre (name) VALUES (@name) RETURNING id, name", Conn);

            command.Parameters.AddWithValue("name", name);

            var reader = command.ExecuteReader();

            if (reader.HasRows && reader.Read())
            {
                Genre newGenre = new(reader.GetInt32(reader.GetOrdinal("id")), reader.GetString(reader.GetOrdinal("name")));

                if (idMovies is not null && idMovies.Any())
                {
                    Conn.Close();
                    return AddMovies(idMovies, newGenre.Id);
                }

                Conn.Close();
                return newGenre;

            }

            return null;
        }

        public Genre? AddMovies(IEnumerable<int> idsMovies, int idGenre)
        {
            if (idsMovies is not null && idsMovies.Any())
            {
                foreach (int idMovie in idsMovies)
                {
                    try
                    {
                        Conn.Open();

                        var command = new NpgsqlCommand(@"
                        WITH inserted AS (
                            INSERT INTO movie_genre ( movie_id, genre_id )
                            VALUES ( @movie_id, @genre_id )
                            RETURNING movie_id, genre_id
                        )
                        SELECT 
                            m.id AS id_movie,
                            m.name AS name_movie,
                            m.description AS description_movie,
                            m.duration AS duration_movie,
                            m.release AS release_movie,
                            g.id AS id_genre,
                            g.name AS name_genre
                        FROM inserted i
                        JOIN movie m ON i.movie_id = m.id
                        JOIN genre g ON i.genre_id = g.id
                        WHERE g.id = i.genre_id
                    ", Conn);

                        command.Parameters.AddWithValue("movie_id", idMovie);
                        command.Parameters.AddWithValue("genre_id", idGenre);

                        var reader = command.ExecuteReader();

                        IEnumerable<Genre> genreOfMovie = [];

                        while (reader.HasRows && reader.Read())
                        {
                            Genre newGenre = new(reader.GetInt32(reader.GetOrdinal("id_genre")), reader.GetString(reader.GetOrdinal("name_genre")));

                            Movie movieGenre = new(
                                    reader.GetInt32(reader.GetOrdinal("id_movie")),
                                    reader.GetString(reader.GetOrdinal("name_movie")),
                                    genreOfMovie.Append(newGenre),
                                    reader.GetInt32(reader.GetOrdinal("duration_movie")),
                                    reader.GetDateTime(reader.GetOrdinal("release_movie")),
                                    reader.GetString(reader.GetOrdinal("description_movie"))
                                );


                            newGenre.Movies.Append(movieGenre);

                            return newGenre;
                        }

                        Conn.Close();
                    } catch {}
                }
            }

            return null;
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