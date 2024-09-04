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
using PlooCinema.WebApi.Repositories;

namespace PlooCinema.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GenreController (IGenreRepository genreRepository) : ControllerBase
    {
        private readonly IGenreRepository _genreRepository = genreRepository;

        [HttpGet]
        public ActionResult<IEnumerable<Genre>> Get([FromQuery(Name = "name")] string? name)
        {
            if (string.IsNullOrEmpty(name))
                return Ok(_genreRepository.SearchAll());

            return Ok(_genreRepository.SearchByName(name));
        }

        [HttpGet("{id}")]
        public ActionResult<Genre> GetById(int id)
        {
            var genre = _genreRepository.SearchById(id);

            if (genre is null)
                return NotFound();
            
            return Ok(genre);
        }

        [HttpPost]
        public ActionResult<Genre> Create(string name, IEnumerable<int> idsMovies)
        {
            try
            {
                var created = _genreRepository.Create(name.ToUpper(), idsMovies);

                if (created is null)
                    return NotFound();
                
                return CreatedAtAction( nameof(GetById), new { id = created.Id }, created);

            } catch (Exception error)
            {
                return BadRequest(error);
            }
        }

        [HttpPut("{id}")]
        public ActionResult<Genre> Update(int id, string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                var genreUpdated = _genreRepository.Update(id, name.ToUpper());

                if ( genreUpdated is null )
                    return NotFound();
                
                return Ok(genreUpdated);
            }

            return BadRequest();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var movieValidation = _genreRepository.SearchById(id);

            if ( movieValidation is null )
                return NotFound();

            _genreRepository.Delete(id);

            return NoContent();
        }
    }
}