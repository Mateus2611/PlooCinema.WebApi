using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlooCinema.Core.DTOs;
using PlooCinema.Core.Responses;
using PlooCinema.Core.Services.Interfaces;

namespace PlooCinema.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GenresController(IGenreService genreService) : ControllerBase
    {
        private readonly IGenreService genreService = genreService;

        [HttpGet]
        [Authorize(Roles = "employee, admin")]
        public async Task<ActionResult<IEnumerable<GenreResponse>>> GetAsync([FromQuery(Name = "name")] string? name, [FromQuery]int skip = 0, [FromQuery] int take = 5)
        {
            if (string.IsNullOrEmpty(name))
                return Ok(await genreService.GetAllAsync(skip, take));

            return Ok(await genreService.GetByNameAsync(name, skip, take));
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "employee, admin")]
        public async Task<ActionResult<GenreResponse>> GetByIdAsync([FromRoute]Guid id)
        {
            var genre = await genreService.GetByIdAsync(id);

            if (genre is null)
                return NotFound();

            return Ok(genre);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<GenreResponse>> CreateAsync(GenreDTO genreDTO)
        {
            try
            {
                genreDTO.Name = genreDTO.Name.ToUpper();
                var created = await genreService.CreateAsync(genreDTO);

                if (created is null)
                    return NotFound();

                return CreatedAtAction(nameof(GetByIdAsync), new { created.Id }, created);
            }
            catch (Exception error)
            {
                return BadRequest(error.Message);
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<GenreResponse>> UpdateAsync([FromRoute] Guid id, GenreDTO genreDTO)
        {
            genreDTO.Name = genreDTO.Name.ToUpper();
            var genreUpdated = await genreService.UpdateAsync(id, genreDTO);

            if (genreUpdated is null)
                return NotFound();

            return Ok(genreUpdated);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> DeleteAsync([FromRoute] Guid id)
        {
            try
            {
                await genreService.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}