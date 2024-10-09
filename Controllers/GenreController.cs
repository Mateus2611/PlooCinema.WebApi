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
using PlooCinema.WebApi.Services.Interfaces;

namespace PlooCinema.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GenreController(IGenreService genreService) : ControllerBase
    {
        private readonly IGenreService genreService = genreService;

        [HttpGet]
        public ActionResult<IEnumerable<Genre>> Get([FromQuery(Name = "name")] string? name)
        {
            if ( string.IsNullOrEmpty(name) )
                return Ok(genreService.GetAll());

            return Ok(genreService.GetByName(name));
        }

        [HttpGet("{id}")]
        public ActionResult<Genre> GetById(int id)
        {
            var genre = genreService.GetById(id);

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
                var created = genreService.Create(genre);

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
            genre.Name = genre.Name.ToUpper();
            var genreUpdated = genreService.Update(genre);

            if (genreUpdated is null)
                return NotFound();

            return Ok(genreUpdated);
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            var genreDelete = genreService.GetById(id);

            if ( genreDelete is null )
                return NotFound();

            genreService.Delete(genreDelete);
            return NoContent();
        }
    }
}