using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PlooCinema.WebApi.Models;
using PlooCinema.WebApi.Services;
using PlooCinema.WebApi.Services.Interfaces;
using PlooCinema.WebApi.Models.Responses;
using PlooCinema.WebApi.Models.DTOs;

namespace PlooCinema.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoomsController(IRoomServices roomServices) : ControllerBase
    {
        private readonly IRoomServices roomServices = roomServices;

        [HttpGet]
        public ActionResult<IEnumerable<RoomResponse>> Get([FromQuery(Name = "Name")] string? name)
        {
            if (string.IsNullOrEmpty(name))
                return Ok(roomServices.GetAll());
            
            return Ok(roomServices.GetByName(name));
        }

        [HttpGet("{id}")]
        public ActionResult<RoomResponse> GetById(int id)
        {
            var room = roomServices.GetById(id);

            if ( room is null )
                return NotFound();
            
            return Ok(room);
        }

        [HttpPost]
        public ActionResult<RoomResponse> Create(RoomDTO room)
        {
            try
            {
                var created = roomServices.Create(room);

                if ( created is null )
                    return NotFound();
                
                return CreatedAtAction( nameof(GetById), new { created.Id }, created);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public ActionResult<RoomResponse> Update([FromRoute] int id, [FromBody] RoomDTO room)
        {
            var roomUpdated = roomServices.Update(id, room);

            if ( roomUpdated is null )
                return NotFound();
            
            return Ok(roomUpdated);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute] int id)
        {
            roomServices.Delete(id);
            return NoContent();
        }
    }
}