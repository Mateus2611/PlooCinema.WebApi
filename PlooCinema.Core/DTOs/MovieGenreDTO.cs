using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PlooCinema.Core.DTOs
{
    public class MovieGenreDTO
    {
        public MovieGenreDTO(IEnumerable<Guid> genresIds, Guid movieId)
        {
            GenresIds = genresIds;
            MovieId = movieId;
        }
        
        public IEnumerable<Guid> GenresIds { get; set; } = [];
        [JsonIgnore]
        public Guid MovieId { get; set; }
    }
}