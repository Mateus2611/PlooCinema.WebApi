using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PlooCinema.WebApi.Model;

namespace PlooCinema.WebApi.Models.Responses
{
    public class SessionResponse
    {
        public int Id { get; set; }
        public DateTimeOffset StartMovie { get; set; }
        public int SeatsAvailable { get; set; }
        public required virtual Movie Movie { get; set; }
        public required virtual RoomSessionResponse Room { get; set; }
    }
}