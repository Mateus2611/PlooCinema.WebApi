using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PlooCinema.WebApi.Models.DTOs
{
    public class SessionDTO
    {
        public DateTimeOffset StartMovie { get; set; }
        public Guid MovieId { get; set; }
        public Guid RoomId { get; set; }
    }
}