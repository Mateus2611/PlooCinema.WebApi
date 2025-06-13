using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlooCinema.Core.DTOs;

namespace PlooCinema.WebApi.Controllers
{
    [ApiController]
    [Route("api/manager/[controller]")]
    [Authorize(Roles = "admin")]
    public class RolesController : ControllerBase
    {
        private readonly RoleManager<AppRole> _roleManager;

        public RolesController(RoleManager<AppRole> roleManager)
        {
            _roleManager = roleManager;
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole([FromBody] CreateRoleDTO roleDto)
        {
            bool roleExists = await _roleManager.RoleExistsAsync(roleDto.RoleName.ToLower());
            if (roleExists)
            { 
                return ValidationProblem($"Role {roleDto.RoleName} já existe."); 
            }
            else
            {
                AppRole identityRole = new()
                {
                    Name = roleDto.RoleName.ToLower()
                };

                IdentityResult result = await _roleManager.CreateAsync(identityRole);

                if (result.Succeeded)
                    return Created();

                return BadRequest();
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppRole>>> GetAsync()
        {
            IEnumerable<AppRole> result = await _roleManager.Roles.ToListAsync();

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<AppRole>> UpdateRoleAsync([FromRoute] string id,[FromBody] UpdateRoleDTO roleUpdate)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role is null)
                return NotFound();

            role.Id = Guid.Parse(id);
            role.Name = roleUpdate.RoleName.ToLower();

            var result = await _roleManager.UpdateAsync(role);

            if (result.Succeeded)
                return Ok(role);

            return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoleAsync([FromRoute] string id)
        {
            var role = await _roleManager.FindByIdAsync(id);

            if (role is null)
                return NotFound();

            await _roleManager.DeleteAsync(role);

            return NoContent();
        }
    }
}
