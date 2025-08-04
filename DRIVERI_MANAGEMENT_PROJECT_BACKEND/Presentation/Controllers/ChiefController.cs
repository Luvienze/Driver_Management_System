using Microsoft.AspNetCore.Mvc;
using Services;
using Services.Contracts;

namespace Presentation.Controllers
{
    [Route("/chief")]
    public class ChiefController : ControllerBase
    {
        private readonly IServiceManager _manager;
        private readonly ILoggerService _logger;
        public ChiefController(IServiceManager manager, ILoggerService logger)
        {
            _manager = manager;
            _logger = logger;
        }

        [HttpPost]
        [Route("find")]
        public IActionResult GetChieFById([FromQuery] int id)
        {
            try
            {
                var chief = _manager.ChiefService.GetChiefById(id);
                return Ok(chief);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });

            }
        }

        [HttpPost]
        [Route("find/registrationNumber")]
        public IActionResult GetChiefByRegistrationNumber([FromQuery] string registrationNumber)
        {
            try
            {
                var chief = _manager.ChiefService.GetChiefByRegistrationNumber(registrationNumber);
                return Ok(chief);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost]
        [Route("find/driver")]
        public IActionResult GetDriverChiefByRegistrationNumber([FromQuery] string registrationNumber)
        {
            try
            {
                var chief = _manager.ChiefService.GetPersonChiefByRegistrationNumber(registrationNumber);
                _logger.LogInfo("chief:" + chief);
                return Ok(chief);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });

            }
        }

        [HttpPost]
        [Route("list/name")]
        public IActionResult GetAllChiefList()
        {
            try
            {
                var chiefs = _manager.ChiefService.GetActiveChiefs();
                return Ok(chiefs);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });

            }
        }

    }
}
