using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PlooCinema.WebApi.Model;
using PlooCinema.WebApi.Repositories;

namespace PlooCinema.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MovieController(IMovieRepository movieRepository) : ControllerBase
    {
        private readonly IMovieRepository movieRepository = movieRepository;

        [HttpGet]
        public ActionResult<IEnumerable<Movie>> Get([FromQuery( Name = "name")] string? name)
        {
            if ( string.IsNullOrEmpty( name ) )
                return Ok(movieRepository.GetAll());
            
            return Ok(movieRepository.GetByName(name));
        }

        [HttpGet("{id}")]
        public ActionResult<Movie> GetById (int id)
        {
            var searchMovie = movieRepository.GetById(id);

            if ( searchMovie is null)
                return NotFound();
            
            return Ok(searchMovie);
        }

        [HttpPost]
        public ActionResult<Movie> Create(Movie movie)
        {
            try
            {
                var create = movieRepository.Create(movie);
                if (create is null)
                    return NotFound();

                return CreatedAtAction( nameof(GetById), new { id = create.Id}, create);
            }
            catch (Exception error)
            {
                return BadRequest(error.Message);
            }
        }

        [HttpPut]
        public ActionResult<Movie> Update(Movie movie)
        {
            try
            {
                var updatedValue = movieRepository.Update(movie);
                
                if (updatedValue is null)
                    return NotFound();

                return Ok(updatedValue);
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var movieDelete = movieRepository.GetById(id);

            if ( movieDelete is null )
                return NotFound();

            movieRepository.Delete(movieDelete);
            return NoContent();
        }
    }
}