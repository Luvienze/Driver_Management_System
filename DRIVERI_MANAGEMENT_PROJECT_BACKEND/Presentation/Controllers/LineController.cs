using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("/line")]
    public class LineController : ControllerBase
    {
        private readonly IServiceManager _manager;
        
        public LineController(IServiceManager manager)
        {
            _manager = manager;
        }

        [HttpPost]
        [Route("list")]
        public IActionResult ListAllLines()
        {
            try
            {
                var lines = _manager.LineService.GetAllLines(trackChanges: false);
                return Ok(lines);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        [Route("find/code")]
        public IActionResult FindLineByLineCode([FromQuery] string lineCode)
        {
            try
            {
                var line = _manager.LineService.GetLineByLineCode(lineCode);
                return Ok(line);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
