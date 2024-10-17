using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PlooCinema.WebApi.Models.Responses
{
    public class GenreResponse
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Informe o nome do gênero.")]
        public required string Name { get; set; }
    }
}