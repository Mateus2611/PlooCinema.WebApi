using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PlooCinema.Core.Responses
{
    public class RoomResponse
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public int Seats { get; set; }
    }
}