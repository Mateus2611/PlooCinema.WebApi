using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlooCinema.WebApi.Models.Responses
{
    public class RoomResponse
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public int Seats { get; set; }
        public virtual IEnumerable<Session> UpCommingSessions { get; set; } = [];
    }
}