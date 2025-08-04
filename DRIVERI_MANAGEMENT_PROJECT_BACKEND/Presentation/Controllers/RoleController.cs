using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace Presentation.Controllers
{

    [ApiController]
    [Route("/role")]
    public class RoleController : ControllerBase
    {
        private readonly IServiceManager _manager;

        public RoleController(IServiceManager manager)
        {
            _manager = manager;
        }

        [HttpPost]
        [Route("list")]
        public IActionResult ListAllRoles()
        {
            try
            {
                var roles = _manager.RoleService.GetAllRoles(false);
                return Ok(roles);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");

            }
        }

        [HttpPost]
        [Route("findByPersonId")]
        public IActionResult FindRolesByPersonId([FromQuery] int personId)
        {
            try
            {
                var role = _manager.RoleService.GetRoleByPersonId(personId, false);
                return Ok(role);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");

            }
        }

        [HttpPost]
        [Route("login")]
        public IActionResult Login([FromQuery] string regNo,
            [FromQuery] string phone)
        {
            try
            {
                var roles = _manager.RoleService.GetRolesByRegistrationNumberAndPhone(regNo, phone);
                if (roles == null || !roles.Any())
                {
                    return NotFound("No roles found for the provided registration number and phone.");
                }
                return Ok(roles);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

    }
}
