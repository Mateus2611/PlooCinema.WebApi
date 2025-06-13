using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PlooCinema.Core.Responses
{
    public class GenreResponse
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Informe o nome do gênero.")]
        public required string Name { get; set; }
    }
}