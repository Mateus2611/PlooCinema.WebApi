using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PlooCinema.WebApi.Model;
using PlooCinema.WebApi.Models.DTOs;
using PlooCinema.WebApi.Models.Responses;
using PlooCinema.WebApi.Repositories;
using PlooCinema.WebApi.Services.Interfaces;

namespace PlooCinema.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MoviesController(IMovieService movieService) : ControllerBase
    {
        private readonly IMovieService movieService = movieService;

        [HttpGet("Skip/{skip}/Take/{take}")]
        public async Task<ActionResult<IEnumerable<GetMovieResponse>>> GetAsync([FromQuery(Name = "name")] string? name, [FromRoute] int skip = 0, [FromRoute] int take = 5)
        {
            if (string.IsNullOrEmpty(name))
                return Ok(await movieService.GetAllAsync(skip, take));

            return Ok(await movieService.GetByNameAsync(name, skip, take));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetMovieResponse>> GetByIdAsync([FromRoute]Guid id)
        {
            var searchMovie = await movieService.GetByIdAsync(id);

            if (searchMovie is null)
                return NotFound();

            return Ok(searchMovie);
        }

        [HttpPost]
        public async Task<ActionResult<GetMovieResponse>> CreateAsync(CreateMovieDTO movie)
        {
            try
            {
                var create = await movieService.CreateAsync(movie);
                if (create is null)
                    return NotFound();

                return CreatedAtAction(nameof(GetByIdAsync), new { id = create.Id }, create);
            }
            catch (Exception error)
            {
                return BadRequest(error.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<UpdateMovieDTO>> UpdateAsync([FromRoute] Guid id, [FromBody] UpdateMovieDTO movie)
        {
            try
            {
                var updatedValue = await movieService.UpdateAsync(id, movie);

                if (updatedValue is null)
                    return NotFound();

                return Ok(updatedValue);
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
        {
            await movieService.DeleteAsync(id);
            return NoContent();
        }

        [HttpPut("{id}/Genres")]
        public async Task<ActionResult<GetMovieResponse>> AddGenreAsync([FromRoute] Guid id, [FromBody] MovieGenreDTO movieGenresIds)
        {
            movieGenresIds.MovieId = id;
            var movie = await movieService.AddGenreAsync(movieGenresIds);

            if (movie is null)
                return NotFound();

            return Ok(movie);
        }

        [HttpDelete("{id}/Genres")]
        public async Task<ActionResult<GetMovieResponse>> RemoveGenreAsync([FromRoute] Guid id, [FromBody] MovieGenreDTO movieGenresIds)
        {
            movieGenresIds.MovieId = id;
            var movie = await movieService.RemoveGenreAsync(movieGenresIds);

            if (movie is null)
                return NotFound();

            return Ok(movie);
        }
    }
}