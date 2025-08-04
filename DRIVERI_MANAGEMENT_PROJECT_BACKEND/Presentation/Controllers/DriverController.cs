using Entities.DataTransferObjects;
using Entities.Wrapper;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;


namespace Presentation.Controllers
{
    [Route("driver")]
    public class DriverController : ControllerBase
    {
        private readonly IServiceManager _manager;
        public DriverController(IServiceManager manager)
        {
            _manager = manager;
        }

        [HttpPost]
        [Route("list")]
        public IActionResult GetAllDrivers()
        {
            try
            {
                var drivers = _manager.DriverService.GetAllDrivers(false);
                return Ok(drivers);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("list/active")]
        public IActionResult GetActiveDriversByChief([FromForm] int chiefId)
        {
            try
            {
                var drivers = _manager.DriverService.GetActiveDriversByChief(chiefId, true);
                return Ok(drivers);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("find/registrationNumber")]
        public IActionResult GetDriverByRegistrationNumber([FromQuery] string registrationNumber)
        {
            try
            {
                var driver = _manager.DriverService.GetDriverByRegistrationNumber(registrationNumber);
                return Ok(driver);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("add")]
        public IActionResult AddNewDriver([FromBody] PersonDriverRequestDto personDriverRequestDto) 
        {
            DriverDto driverDto = personDriverRequestDto.DriverDto;
            PersonDto personDto = personDriverRequestDto.PersonDto;
            try
            {
                _manager.DriverService.SaveOrUpdateDriver(driverDto);
                _manager.PersonService.SaveOrUpdate(personDto);
                return Ok("Driver and person added successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("set/active")]
        public IActionResult SetIsActive([FromBody] DriverDto driverDto)
        {
            if (driverDto == null)
                return BadRequest("DriverDto is null.");

            try
            {
                _manager.DriverService.UpdateIsActive(driverDto.IsActive, driverDto.Id);
                return Ok(driverDto); 
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        [Route("update")]
        public IActionResult UpdateDriverByRegistrationNumber([FromBody] DriverDto driverDto)
        {
            try
            {
                _manager.DriverService.UpdateDriverByRegistrationNumber(driverDto);
                return Ok("Driver updated successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("daily/dayOff")]
        public IActionResult GetDriverOnDayOff([FromQuery] int day)
        {
            try
            {
                int count = _manager.DriverService.GetDriverOnDayOff(day);
                return Ok(count);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("daily/gender")]
        public IActionResult GetDriverGenderCount([FromQuery] int gender)
        {
            try
            {
                int count = _manager.DriverService.GetDriverGenderCount(gender);
                return Ok(count);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

      
    }
}
