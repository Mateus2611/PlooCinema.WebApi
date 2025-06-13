using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PlooCinema.Core.DTOs
{
    public class RoomDTO
    {
        [Required(ErrorMessage = "Informe o nome da sala")]
        public required string Name { get; set; }
        public int Seats { get; set; }
    }
}