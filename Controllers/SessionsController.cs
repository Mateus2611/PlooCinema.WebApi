using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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

        [HttpGet]
        public ActionResult<IEnumerable<SessionResponse>> Get()
            => Ok(sessionService.GetAll());

        [HttpGet("{id}")]
        public ActionResult<SessionResponse> GetById([FromRoute] Guid id)
        {
            var session = sessionService.GetById(id);

            if (session is null)
                return NotFound();
            
            return Ok(session);
        }

        [HttpPost]
        public ActionResult<SessionResponse> Create([FromBody]SessionDTO session)
        {
            try
            {
                var created = sessionService.Create(session);

                if (created is null)
                    return NotFound();
                
                return CreatedAtAction(nameof(GetById), new {created.Id}, created);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut("{id}")]
        public ActionResult<SessionResponse> Update([FromRoute] Guid id, [FromBody] SessionDTO session)
        {
            var updatedSession = sessionService.Update(id, session);

            if (updatedSession is null)
                return NotFound();
            
            return Ok(updatedSession);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute] Guid id)
        {
            sessionService.Delete(id);
            return NoContent();
        }
    }
}