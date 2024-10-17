using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PlooCinema.WebApi.Models.DTOs
{
    public class MovieGenreDTO
    {
        public IEnumerable<int> GenresIds { get; set; } = [];
        [JsonIgnore]
        public int MovieId { get; set; }
    }
}