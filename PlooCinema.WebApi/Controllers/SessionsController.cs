using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PlooCinema.Core.DTOs;
using PlooCinema.Core.Responses;
using PlooCinema.Core.Services.Interfaces;

namespace PlooCinema.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SessionsController(ISessionService sessionService) : ControllerBase
    {
        private readonly ISessionService sessionService = sessionService;

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<SessionResponse>>> Get([FromQuery] Guid? movieId = null, [FromQuery] Guid? roomId = null, [FromQuery] int skip = 0, [FromQuery] int take = 5)
        {
            if (movieId is not null || roomId is not null)
                return Ok(await sessionService.GetSessionsFilteredByMovieAndRoomAsync(movieId, roomId, skip, take));

            return Ok(await sessionService.GetAllAsync(skip, take));
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<SessionResponse>> GetByIdAsync([FromRoute] Guid id)
        {
            var session = await sessionService.GetByIdAsync(id);

            if (session is null)
                return NotFound();

            return Ok(session);
        }

        [HttpPost]
        [Authorize(Roles = "employee, admin")]
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
        [Authorize(Roles = "employee, admin")]
        public async Task<ActionResult<SessionResponse>> UpdateAsync([FromRoute] Guid id, [FromBody] SessionDTO session)
        {
            var updatedSession = await sessionService.UpdateAsync(id, session);

            if (updatedSession is null)
                return NotFound();

            return Ok(updatedSession);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "employee, admin")]
        public async Task<ActionResult> DeleteAsync([FromRoute] Guid id)
        {
            await sessionService.DeleteAsync(id);
            return NoContent();
        }

        [HttpPut("ReserveSeats/{id}")]
        [Authorize(Roles = "employee")]
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
        [Authorize(Roles = "employee")]
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