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
    public class GenreController(IGenreRepository genreRepository) : ControllerBase
    {
        private readonly IGenreRepository genreRepository = genreRepository;

        [HttpGet]
        public ActionResult<IEnumerable<Genre>> Get([FromQuery(Name = "name")] string? name)
        {
            if ( string.IsNullOrEmpty(name) )
                return Ok(genreRepository.GetAll());

            return Ok(genreRepository.GetByName(name));
        }

        [HttpGet("{id}")]
        public ActionResult<Genre> GetById(int id)
        {
            var genre = genreRepository.GetById(id);

            if ( genre is null)
                return NotFound();
            
            return Ok(genre);
        }

        [HttpPost]
        public ActionResult<Genre> Create(Genre genre)
        {
            try
            {
                genre.Name = genre.Name.ToUpper();
                var created = genreRepository.Create(genre);

                if (created is null)
                    return NotFound();

                return CreatedAtAction(nameof(GetById), new { Id = created.Id}, created);
            }
            catch (Exception error)
            {
                return BadRequest(error.Message);
            }
        }

        [HttpPut]
        public ActionResult<Genre> Update(Genre genre)
        {
            var genreUpdated = genreRepository.Update(genre);

            if (genreUpdated is null)
                return NotFound();

            return Ok(genreUpdated);
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            var genreDelete = genreRepository.GetById(id);

            if ( genreDelete is null )
                return NotFound();

            genreRepository.Delete(genreDelete);
            return NoContent();
        }
    }
}