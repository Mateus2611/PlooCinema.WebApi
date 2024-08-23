using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
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

        [HttpGet("{id}")]
        public ActionResult<Genre> GetById(int id)
        {
            var genre = _genreRepository.SearchById(id);

            if (genre is null)
                return NotFound();
            
            return Ok(genre);
        }

        [HttpPost]
        public ActionResult<Genre> Create(Genre genre)
        {
            try
            {
                var created = _genreRepository.Create(genre);

                if (created is null)
                    return NotFound();
                
                return CreatedAtAction( nameof(GetById), new { id = created.Id }, created);
            } catch (Exception error)
            {
                return BadRequest(error);
            }
        }
    }
}