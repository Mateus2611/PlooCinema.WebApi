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
        public ActionResult<IEnumerable<GenreDTO>> Get([FromQuery(Name = "name")] string? name)
        {
            if ( string.IsNullOrEmpty(name) )
                return Ok(genreService.GetAll());

            return Ok(genreService.GetByName(name));
        }

        [HttpGet("{id}")]
        public ActionResult<GenreDTO> GetById(int id)
        {
            var genre = genreService.GetById(id);

            if ( genre is null)
                return NotFound();
            
            return Ok(genre);
        }

        [HttpPost]
        public ActionResult<GenreDTO> Create(GenreDTO genreDTO)
        {
            try
            {
                genreDTO.Name = genreDTO.Name.ToUpper();
                var created = genreService.Create(genreDTO);

                if (created is null)
                    return NotFound();

                return CreatedAtAction(nameof(GetById), new { created.Id }, created);
            }
            catch (Exception error)
            {
                return BadRequest(error.Message);
            }
        }

        [HttpPut]
        public ActionResult<GenreDTO> Update(GenreDTO genreDTO)
        {
            genreDTO.Name = genreDTO.Name.ToUpper();
            var genreUpdated = genreService.Update(genreDTO);

            if (genreUpdated is null)
                return NotFound();

            return Ok(genreUpdated);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute]int id)
        {
            var genreDelete = genreService.GetById(id);

            if ( genreDelete is null )
                return NotFound();

            genreService.Delete(genreDelete);
            return NoContent();
        }
    }
}