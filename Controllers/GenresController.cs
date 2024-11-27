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
        public ActionResult<IEnumerable<GenreResponse>> Get([FromQuery(Name = "name")] string? name)
        {
            if (string.IsNullOrEmpty(name))
                return Ok(genreService.GetAll());

            return Ok(genreService.GetByName(name));
        }

        [HttpGet("{id}")]
        public ActionResult<GenreResponse> GetById([FromRoute]Guid id)
        {
            var genre = genreService.GetById(id);

            if (genre is null)
                return NotFound();

            return Ok(genre);
        }

        [HttpPost]
        public ActionResult<GenreResponse> Create(GenreDTO genreDTO)
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

        [HttpPut("{id}")]
        public ActionResult<GenreResponse> Update([FromRoute] Guid id, GenreDTO genreDTO)
        {
            genreDTO.Name = genreDTO.Name.ToUpper();
            var genreUpdated = genreService.Update(id, genreDTO);

            if (genreUpdated is null)
                return NotFound();

            return Ok(genreUpdated);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute] Guid id)
        {
            try
            {
                genreService.Delete(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}