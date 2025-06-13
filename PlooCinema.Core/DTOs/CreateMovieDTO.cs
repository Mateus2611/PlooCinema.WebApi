using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PlooCinema.Core.DTOs
{
    public class CreateMovieDTO
    {
        [Required(ErrorMessage = "Informe o nome do filme.")]
        public required string Name { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Duração inválida. Informe um valor acima de zero")]
        public int Duration { get; set; }

        public DateTimeOffset Release
        {
            get => _release;
            set
            {
                if (value > DateTimeOffset.Now.Date)
                    throw new ArgumentException("A data informada não é valida.");

                _release = value.Date.ToUniversalTime();
            }
        }
        private DateTimeOffset _release { get; set; }

        [Required(ErrorMessage = "Informe a descrição do filme.")]
        public required string Description { get; set; }
        public IEnumerable<Guid> IdsGenres { get; set; } = [];
    }
}