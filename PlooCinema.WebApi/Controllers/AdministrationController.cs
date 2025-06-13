using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PlooCinema.Core.DTOs;

namespace PlooCinema.WebApi.Controllers
{
    [ApiController]
    [Route("api/manager/[Controller]")]
    [Authorize(Roles = "admin")]
    public class AdministrationController : ControllerBase
    {
        private readonly RoleManager<AppRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;

        public AdministrationController(RoleManager<AppRole> roleManager, UserManager<AppUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        [HttpGet("Users")]
        public IActionResult GetUser()
        {
            var users = _userManager.Users;
            return Ok(users);
        }

        [HttpGet("Roles/User")]
        public async Task<ActionResult<List<AppUser>>>GetUserPerRole([FromHeader] string name) => 
            Ok(await _userManager.GetUsersInRoleAsync(name));

        [HttpPost("Users/{id}/Roles")]
        public async Task<ActionResult<IdentityResult>> AddRoleToUser([FromRoute] Guid id, RoleIdDTO userRoleDto)
        {
            var userValidation = await _userManager.FindByIdAsync(id.ToString());
            if (userValidation is null)
                return NotFound();

            var roleValidation = await _roleManager.FindByIdAsync(userRoleDto.RoleId.ToString());
            if (roleValidation is null)
                return NotFound();

            var result = await _userManager.AddToRoleAsync(userValidation, roleValidation.Name);

            if (result.Succeeded is false)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpDelete("Users/{id}/Roles")]
        public async Task<ActionResult<IdentityResult>> RemoveRoleToUser([FromRoute] Guid id, RoleIdDTO userRoleDto)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user is null)
                return NotFound();

            var role = await _roleManager.FindByIdAsync(userRoleDto.RoleId.ToString());
            if (role is null)
                return NotFound();

            var result =  await _userManager.RemoveFromRoleAsync(user, role.Name);

            if (result.Succeeded is false)
                return BadRequest(result);

            return Ok(result);
        }
    }
}
