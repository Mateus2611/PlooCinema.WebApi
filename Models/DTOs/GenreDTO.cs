using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PlooCinema.WebApi.Models.DTOs
{
    public class GenreDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Informe o nome do gênero.")]
        public string Name { get; set; } = string.Empty;
    }
}