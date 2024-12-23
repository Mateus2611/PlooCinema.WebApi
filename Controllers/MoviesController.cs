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

        [HttpGet]
        public ActionResult<IEnumerable<GetMovieResponse>> Get([FromQuery(Name = "name")] string? name)
        {
            if (string.IsNullOrEmpty(name))
                return Ok(movieService.GetAll());

            return Ok(movieService.GetByName(name));
        }

        [HttpGet("{id}")]
        public ActionResult<GetMovieResponse> GetById([FromRoute]Guid id)
        {
            var searchMovie = movieService.GetById(id);

            if (searchMovie is null)
                return NotFound();

            return Ok(searchMovie);
        }

        [HttpPost]
        public ActionResult<GetMovieResponse> Create(CreateMovieDTO movie)
        {
            try
            {
                var create = movieService.Create(movie);
                if (create is null)
                    return NotFound();

                return CreatedAtAction(nameof(GetById), new { id = create.Id }, create);
            }
            catch (Exception error)
            {
                return BadRequest(error.Message);
            }
        }

        [HttpPut("{id}")]
        public ActionResult<UpdateMovieDTO> Update([FromRoute] Guid id, [FromBody] UpdateMovieDTO movie)
        {
            try
            {
                var updatedValue = movieService.Update(id, movie);

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
        public IActionResult Delete([FromRoute] Guid id)
        {
            movieService.Delete(id);
            return NoContent();
        }

        [HttpPut("{id}/Genres")]
        public ActionResult<GetMovieResponse> AddGenre([FromRoute] Guid id, [FromBody] MovieGenreDTO movieGenresIds)
        {
            movieGenresIds.MovieId = id;
            var movie = movieService.AddGenre(movieGenresIds);

            if (movie is null)
                return NotFound();

            return Ok(movie);
        }

        [HttpDelete("{id}/Genres")]
        public ActionResult<GetMovieResponse> RemoveGenre([FromRoute] Guid id, [FromBody] MovieGenreDTO movieGenresIds)
        {
            movieGenresIds.MovieId = id;
            var movie = movieService.RemoveGenre(movieGenresIds);

            if (movie is null)
                return NotFound();

            return Ok(movie);
        }
    }
}