using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PlooCinema.WebApi.Models;
using PlooCinema.WebApi.Services;
using PlooCinema.WebApi.Services.Interfaces;

namespace PlooCinema.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoomsController(IRoomServices roomServices) : ControllerBase
    {
        private readonly IRoomServices roomServices = roomServices;

        [HttpGet]
        public ActionResult<IEnumerable<Room>> Get([FromQuery(Name = "Name")] string? name)
        {
            if (string.IsNullOrEmpty(name))
                return Ok(roomServices.GetAll());
            
            return Ok(roomServices.GetByName(name));
        }

        [HttpGet("{id}")]
        public ActionResult<Room> GetById(int id)
        {
            var room = roomServices.GetById(id);

            if ( room is null )
                return NotFound();
            
            return Ok(room);
        }

        [HttpPost]
        public ActionResult<Room> Create(Room room)
        {
            try
            {
                var created = roomServices.Create(room);

                if ( created is null )
                    return NotFound();
                
                return CreatedAtAction( nameof(GetById), new { Id = created.Id}, created);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public ActionResult<Room> Update(Room room)
        {
            var roomUpdated = roomServices.Update(room);

            if ( roomUpdated is null )
                return NotFound();
            
            return Ok(roomUpdated);
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            var roomDelete = roomServices.GetById(id);

            if ( roomDelete is null )
                return NotFound();
            
            roomServices.Delete(roomDelete);
            return NoContent();
        }
    }
}