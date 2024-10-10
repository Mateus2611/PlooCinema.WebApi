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
using PlooCinema.WebApi.Services.Interfaces;

namespace PlooCinema.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MoviesController(IMovieService movieService) : ControllerBase
    {
        private readonly IMovieService movieService = movieService;

        [HttpGet]
        public ActionResult<IEnumerable<Movie>> Get([FromQuery( Name = "name")] string? name)
        {
            if ( string.IsNullOrEmpty( name ) )
                return Ok(movieService.GetAll());
            
            return Ok(movieService.GetByName(name));
        }

        [HttpGet("{id}")]
        public ActionResult<Movie> GetById (int id)
        {
            var searchMovie = movieService.GetById(id);

            if ( searchMovie is null)
                return NotFound();
            
            return Ok(searchMovie);
        }

        [HttpPost]
        public ActionResult<Movie> Create(Movie movie)
        {
            try
            {
                var create = movieService.Create(movie);
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
                var updatedValue = movieService.Update(movie);
                
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
            var movieDelete = movieService.GetById(id);

            if ( movieDelete is null )
                return NotFound();

            movieService.Delete(movieDelete);
            return NoContent();
        }

        [HttpPut("{idMovie}/Genres")]
        public ActionResult<Movie> AddGenre(int idMovie, [FromBody] int idGenre)
        {
            var movie = movieService.AddGenre(idMovie, idGenre);

            if ( movie is null )
                return NotFound();

            return Ok(movie);
        }
        [HttpDelete("{idMovie}/Genres")]
        public ActionResult<Movie> RemoveGenre(int idMovie, [FromBody] int idGenre)
        {
            var movie = movieService.RemoveGenre(idMovie, idGenre);

            if ( movie is null )
                return NotFound();

            return Ok(movie);
        }
    }
}