using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlooCinema.WebApi.Models.DTOs
{
    public class GetMovieResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Duration { get; set; }
        public DateTimeOffset Release { get; set; }
        public string Description { get; set; } = string.Empty;
        public virtual ICollection<Genre> Genres { get; set; } = [];
    }
}