using Microsoft.AspNetCore.Mvc;
using Services;
using Services.Contracts;

namespace Presentation.Controllers
{
    [Route("vehicle")]
    public class VehicleController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public VehicleController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [HttpPost]
        [Route("list")]
        public IActionResult GetAllVehicles()
        {
            try
            {
                var vehicles = _serviceManager.VehicleService.GetAllVehicles(false);
                return Ok(vehicles);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost]
        [Route("find")]
        public IActionResult GetVehicleByDoorNo([FromQuery] string doorNo)
        {
            try
            {
                var vehicle = _serviceManager.VehicleService.GetVehicleByDoorNo(doorNo);
                if (vehicle == null)
                {
                    return NotFound(new { message = "Vehicle not found" });
                }
                return Ok(vehicle);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost]
        [Route("list/garages/id")]
        public IActionResult GetVehiclesByGarageId([FromQuery] int id)
        {
            try
            {
                var chiefDto = _serviceManager.ChiefService.GetChiefById(id);
                var vehicles = _serviceManager.VehicleService.GetVehicleListByGarageId(chiefDto.GarageId);
                return Ok(vehicles);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost]
        [Route("find/status")]
        public IActionResult GetVehicleCountByStatus([FromQuery] int status)
        {
            try
            {
                var count = _serviceManager.VehicleService.GetVehicleCountByStatus(status);
                return Ok(new { count });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
