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
        // public DateTimeOffset StartMovie
        // {
        //     get => _startMovie;
        //     set
        //     {
        //         _startMovie = value.Date.ToUniversalTime();
        //     }
        // }
        private DateTimeOffset _startMovie { get; set; }
        public int MovieId { get; set; }
        public int RoomId { get; set; }
    }
}