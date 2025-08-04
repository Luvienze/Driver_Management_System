using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Models;
using Entities.Wrapper;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("/person")]
    public class PersonController : ControllerBase
    {

        private readonly IServiceManager _manager;
        private readonly IMapper _mapper;

        public PersonController(IServiceManager manager, IMapper mapper)
        {
            _manager = manager;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("find")]
        public IActionResult GetPersonByDriverId([FromQuery] int id)
        {
            try
            {
                var person = _manager.PersonService.GetPersonByDriverId(id);
                return Ok(person);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        [Route("find/registrationNumber")]
        public IActionResult GetPersonByRegistrationNo([FromForm] string registrationNumber)
        {
            try
            {
                var person = _manager.PersonService.GetPersonByRegistrationNumber(registrationNumber);
                return Ok(person);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        [Route("find/driver")]
        public IActionResult GetDriverPersonInfoByRegistrationNumber([FromQuery] string registrationNo)
        {
            try
            {
                var personDto = _manager.PersonService.GetPersonByRegistrationNumber(registrationNo);
                var driverDto = _manager.DriverService.GetDriverByRegistrationNumber(registrationNo);
                PersonDriverRequestDto personDriverRequestDto = new()
                {
                    PersonDto = _mapper.Map<PersonDto>(personDto),
                    DriverDto = driverDto
                };
                return Ok(personDriverRequestDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        [Route("list")]
        public IActionResult GetAllPersons()
        {
            try
            {
                var persons = _manager.PersonService.GetAllPersons(false);
                return Ok(persons);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        [Route("saveOrUpdate")]
        public IActionResult SaveOrUpdatePerson([FromBody] PersonDto personDto)
        {
            try
            {
                if (personDto == null)
                {
                    return BadRequest("Person data is null.");
                }
                _manager.PersonService.SaveOrUpdate(personDto);
                return Ok(personDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        [Route("delete")]
        public IActionResult DeletePerson([FromForm] string registrationNumber)
        {
            try
            {
                var person = _manager.PersonService.GetPersonByRegistrationNumber(registrationNumber);
                _manager.PersonService.DeletePerson(person);
                return Ok($"Person with registration number {registrationNumber} deleted successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
