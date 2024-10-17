using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlooCinema.WebApi.Models.Responses
{
    public class GetMovieResponse
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public int Duration { get; set; }
        public DateTimeOffset Release { get; set; }
        public required string Description { get; set; }
        public virtual ICollection<Genre> Genres { get; set; } = [];
    }
}