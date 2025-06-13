using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PlooCinema.Core.Models;

namespace PlooCinema.Core.Responses
{
    public class MovieSessionResponse
    {
        public required string Name { get; set; }
        public required string Description { get; set; }
        public IEnumerable<Genre> Genres { get; set; } = [];
    }
}