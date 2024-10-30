using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlooCinema.WebApi.Models.Responses
{
    public class MovieSessionResponse
    {
        public required string Name { get; set; }
        public required string Description { get; set; }
        public virtual IEnumerable<Genre> Genres { get; set; } = [];
    }
}