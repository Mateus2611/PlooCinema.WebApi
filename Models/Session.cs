using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PlooCinema.WebApi.Model;

namespace PlooCinema.WebApi.Models
{
    public class Session
    {
        public Session() {}

        public Session(int id, DateTimeOffset startMovie, int seatsAvailable)
        {
            Id = id;
            StartMovie = startMovie;
            SeatsAvailable = seatsAvailable;
        }

        public int Id { get; set; }
        public DateTimeOffset StartMovie { get; set; }
        public int SeatsAvailable { get; set; }
        public virtual Movie Movie { get; set; }
        public virtual Room Room { get; set; }
    }
}