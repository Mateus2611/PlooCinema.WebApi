using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PlooCinema.WebApi.Model;
using PlooCinema.WebApi.Repositories;
using PlooCinema.WebApi.Repositories.PostgreSql;

namespace PlooCinema.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MovieController(IMovieRepository movieRepository) : ControllerBase
    {
        private readonly IMovieRepository _movieRepository = movieRepository;

        [HttpGet]
        public ActionResult<IEnumerable<Movie>> Get([FromQuery(Name = "name")] string? name)
        {
            if (string.IsNullOrEmpty(name))
                return Ok(_movieRepository.SearchAll());

            return Ok(_movieRepository.SearchByName(name));
        }

        [HttpGet("{id}")]
        public ActionResult<Movie> GetById(int id)
        {
            var movie = _movieRepository.SearchById(id);

            if (movie is null)
                return NotFound();

            return Ok(movie);
        }

        [HttpPost]
        public ActionResult<Movie> Create(Movie movie)
        {
            try
            {
                var create = _movieRepository.Create(movie);
                if ( create is null )
                    return NotFound();

                return CreatedAtAction( nameof(GetById), new { id = create.Id }, create);
            } catch (Exception error)
            {
                return BadRequest(error);
            }
        }

        [HttpPut("{id}")]
        public ActionResult<Movie> Update(int id, Movie movie)
        {
            try
            {
                var updatedValue = _movieRepository.Update(id, movie);
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
        public IActionResult Delete(int id)
        {
            var movie = _movieRepository.SearchById(id);
            if ( movie is null )
                return NotFound();
            
            _movieRepository.Delete(id);

            return NoContent();
        }
    }
}