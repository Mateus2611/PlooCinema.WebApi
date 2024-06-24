using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlooCinema.WebApi.Model
{
    public class Movie
    {
        public Movie() { }
        public Movie(int id, string name, string genre, TimeSpan duration, DateOnly release, string description)
        {
            Id = id;
            Name = name;
            Genre = genre;
            Duration = duration;
            Release = release;
            Description = description;
        }
        public Movie(string name, string genre, TimeSpan duration, DateOnly release, string description)
        {
            Name = name;
            Genre = genre;
            Duration = duration;
            Release = release;
            Description = description;
        }
        public int Id { get; set; }
        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                if (value == "" || value == null)
                    throw new ArgumentException("Informe o titulo do filme.");

                _name = value;
            }
        }
        private string _genre;
        public string Genre
        {
            get => _genre;
            set
            {
                if (value == "" || value == null)
                    throw new ArgumentException("Informe o genero do filme.");

                _genre = value.ToUpper();
            }
        }
        private TimeSpan _duration;
        public TimeSpan Duration
        {
            get => _duration;
            set
            {
                if (value == TimeSpan.Zero)
                    throw new ArgumentException("Informe a duração do filme.");

                _duration = value;
            }
        }
        private DateOnly _release;
        public DateOnly Release
        {
            get => _release;
            set
            {
                if (value > DateOnly.FromDateTime(DateTime.Now))
                    throw new ArgumentException("A data informada não é valida.");

                _release = value;
            }
        }
        private string _description;
        public string Description
        {
            get => _description;
            set
            {
                if (value == "" || value == null)
                    throw new ArgumentException("Informe a descrição do filme.");

                _description = value;
            }
        }

        public override string ToString()
        {
            return $"ID: {Id} Nome: {Name}, Gênero: {Genre}, Duração: {Duration}, Lançamento: {Release}, Descrição: {Description}\n";
        }

    }
}