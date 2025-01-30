using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PlooCinema.WebApi.Models;
using PlooCinema.WebApi.Models.DTOs;
using PlooCinema.WebApi.Models.Responses;
using PlooCinema.WebApi.Repositories;
using PlooCinema.WebApi.Services.Interfaces;

namespace PlooCinema.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GenresController(IGenreService genreService) : ControllerBase
    {
        private readonly IGenreService genreService = genreService;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GenreResponse>>> GetAsync([FromQuery(Name = "name")] string? name)
        {
            if (string.IsNullOrEmpty(name))
                return Ok(await genreService.GetAllAsync());

            return Ok(await genreService.GetByNameAsync(name));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GenreResponse>> GetByIdAsync([FromRoute]Guid id)
        {
            var genre = await genreService.GetByIdAsync(id);

            if (genre is null)
                return NotFound();

            return Ok(genre);
        }

        [HttpPost]
        public async Task<ActionResult<GenreResponse>> CreateAsync(GenreDTO genreDTO)
        {
            try
            {
                genreDTO.Name = genreDTO.Name.ToUpper();
                var created = await genreService.CreateAsync(genreDTO);

                if (created is null)
                    return NotFound();

                return CreatedAtAction(nameof(GetByIdAsync), new { created.Id }, created);
            }
            catch (Exception error)
            {
                return BadRequest(error.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<GenreResponse>> UpdateAsync([FromRoute] Guid id, GenreDTO genreDTO)
        {
            genreDTO.Name = genreDTO.Name.ToUpper();
            var genreUpdated = await genreService.UpdateAsync(id, genreDTO);

            if (genreUpdated is null)
                return NotFound();

            return Ok(genreUpdated);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync([FromRoute] Guid id)
        {
            try
            {
                await genreService.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}