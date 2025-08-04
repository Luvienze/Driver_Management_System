using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("/route")]
    public class RouteController : ControllerBase
    {
        private readonly IServiceManager _manager;
        public RouteController(IServiceManager manager)
        {
            _manager = manager;
        }

        [HttpPost]
        [Route("list")]
        public IActionResult GetRouteList()
        {
            try
            {
                var routes = _manager.RouteService
                .GetAllRoutes(false);

                return Ok(routes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }

        }

        [HttpPost]
        [Route("find/name")]
        public IActionResult GetRouteByRouteName([FromQuery] string routeName)
        {
            try
            {
                var route = _manager.RouteService
             .GetRouteByRouteName(routeName, false);
                return Ok(route);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");

            }
        }
    }
}
