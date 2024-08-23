using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Npgsql;
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

        public Genre? Create( Genre genre )
        {
            Conn.Open();

            var command = new NpgsqlCommand( "INSERT INTO genre ( name ) VALUES ( @name ) RETURNING id, name", Conn);

            command.Parameters.AddWithValue("name", genre.Name.ToUpper());

            var reader = command.ExecuteReader();

            if (reader.HasRows && reader.Read())
            {
                Genre savedData = new( reader.GetInt32(reader.GetOrdinal("id")), reader.GetString(reader.GetOrdinal("name")));

                Conn.Close();

                return savedData;
            }

            return null;
        }

        public Genre? SearchById(int id)
        {
            Conn.Open();

            var command = new NpgsqlCommand( "SELECT * FROM genre WHERE id = @id ", Conn);

            command.Parameters.AddWithValue("id", id);

            var reader = command.ExecuteReader();

            if (!reader.HasRows && reader.Read())
            {
                Genre genre = new( reader.GetInt32(reader.GetOrdinal("id")), reader.GetString(reader.GetOrdinal("name")));

                Conn.Close();

                return genre;
            }

            Conn.Close();
            return null;
        }
    }
}