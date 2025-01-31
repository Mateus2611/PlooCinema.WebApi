using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PlooCinema.WebApi.Models;
using PlooCinema.WebApi.Models.DTOs;
using PlooCinema.WebApi.Models.Responses;
using PlooCinema.WebApi.Services.Interfaces;

namespace PlooCinema.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SessionsController(ISessionService sessionService) : ControllerBase
    {
        private readonly ISessionService sessionService = sessionService;

        [HttpGet("Skip/{skip}/Take/{take}")]
        public async Task<ActionResult<IEnumerable<SessionResponse>>> Get([FromRoute] int skip = 0, [FromRoute] int take = 5)
            => Ok(await sessionService.GetAllAsync(skip, take));

        [HttpGet("{id}")]
        public async Task<ActionResult<SessionResponse>> GetByIdAsync([FromRoute] Guid id)
        {
            var session = await sessionService.GetByIdAsync(id);

            if (session is null)
                return NotFound();

            return Ok(session);
        }

        [HttpPost]
        public async Task<ActionResult<SessionResponse>> CreateAsync([FromBody] SessionDTO session)
        {
            try
            {
                var created = await sessionService.CreateAsync(session);

                if (created is null)
                    return NotFound();

                return CreatedAtAction(nameof(GetByIdAsync), new { created.Id }, created);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<SessionResponse>> UpdateAsync([FromRoute] Guid id, [FromBody] SessionDTO session)
        {
            var updatedSession = await sessionService.UpdateAsync(id, session);

            if (updatedSession is null)
                return NotFound();

            return Ok(updatedSession);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync([FromRoute] Guid id)
        {
            await sessionService.DeleteAsync(id);
            return NoContent();
        }

        [HttpPut("ReserveSeats/{id}")]
        public async Task<ActionResult<SessionResponse>> ReserveSeatsAsync([FromRoute(Name = "id")] Guid id, [FromBody] int seats)
        {
            try
            {
                var session = await sessionService.ReserveSeatsAsync(id, seats);

                if (session is null)
                    return NotFound();

                return Ok(session);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut("CancelReservedSeats/{id}")]
        public async Task<ActionResult<SessionResponse>> CancelReservedSeatsAsync([FromRoute(Name = "id")] Guid id, [FromBody] int seats)
        {
            try
            {
                var session = await sessionService.CancelReservedSeatsAsync(id, seats);

                if (session is null)
                    return NotFound();

                return Ok(session);
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}