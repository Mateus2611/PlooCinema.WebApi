using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlooCinema.WebApi.Models.Responses
{
    public class RoomSessionResponse
    {
        public int Id { get; set; }
        public required string Name { get; set; }
    }
}