using Microsoft.AspNetCore.Mvc;
using Services;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("/garage")]
    public class GarageController : ControllerBase
    {
        private readonly IServiceManager _manager;

        public GarageController(IServiceManager manager)
        {
            _manager = manager;
        }

        [HttpPost]
        [Route("list")]
        public IActionResult GetAllGarages()
        {
            try
            {
                var garages = _manager.GarageService.GetAllGarages(false);
                return Ok(garages);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");

            }
        }

        [HttpPost]
        [Route("find/registrationNumber")]
        public IActionResult GetGarageByRegistrationNumber([FromQuery] string registrationNumber)
        {
            try
            {
                var garage = _manager.GarageService.GetGarageByRegistrationNumber(registrationNumber);
                if (garage == null)
                {
                    return NotFound($"Garage with registration number {registrationNumber} not found.");
                }
                return Ok(garage);
            }catch(Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
