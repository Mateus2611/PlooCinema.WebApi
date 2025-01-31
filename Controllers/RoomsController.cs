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

        [HttpGet("Skip/{skip}/Take/{take}")]
        public async Task<ActionResult<IEnumerable<RoomResponse>>> GetAsync([FromQuery(Name = "Name")] string? name, [FromRoute] int skip = 0, [FromRoute] int take = 5)
        {
            if (string.IsNullOrEmpty(name))
                return Ok(await roomServices.GetAllAsync(skip, take));
            
            return Ok(await roomServices.GetByNameAsync(name, skip, take));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RoomResponse>> GetByIdAsync([FromRoute] Guid id)
        {
            var room = await roomServices.GetByIdAsync(id);

            if ( room is null )
                return NotFound();
            
            return Ok(room);
        }

        [HttpPost]
        public async Task<ActionResult<RoomResponse>> CreateAsync(RoomDTO room)
        {
            try
            {
                var created = await roomServices.CreateAsync(room);

                if ( created is null )
                    return NotFound();
                
                return CreatedAtAction( nameof(GetByIdAsync), new { created.Id }, created);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<RoomResponse>> UpdateAsync([FromRoute] Guid id, [FromBody] RoomDTO room)
        {
            var roomUpdated = await roomServices.UpdateAsync(id, room);

            if ( roomUpdated is null )
                return NotFound();
            
            return Ok(roomUpdated);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync([FromRoute] Guid id)
        {
            await roomServices.DeleteAsync(id);
            return NoContent();
        }
    }
}