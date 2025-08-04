using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Exceptions;
using Entities.Models;
using Repositories.Contracts;
using Services.Contracts;

namespace Services
{
    public class PersonManager : IPersonService
    {
        private readonly IRepositoryManager _manager;
        private readonly ILoggerService _logger;
        private readonly IMapper _mapper;

        public PersonManager(IRepositoryManager manager, ILoggerService loggerService,IMapper mapper)
        {
            _manager = manager;
            _logger = loggerService;
            _mapper = mapper;
        }

        public void DeletePerson(PersonDto person)
        {
            if(person is null)
            {
                _logger.LogError("Person object sent from controller is null.");
                throw new NullReferenceException("Person object sent from controller is null.");
            }
            if (person.IsDeleted == true)
            {
                _logger.LogError("Person is already deleted.");
                // throw new PersonAlreadyDeletedException(person.Id);
            }
            var mappedPerson = _mapper.Map<Person>(person);
            _manager.Person.DeletePerson(mappedPerson);
            _manager.Save();
        }

        public IEnumerable<PersonDto> GetAllPersons(bool trackChanges)
        {
            var persons = _manager.Person.GetAllPersons(trackChanges);
            if (persons is null || !persons.Any()) _logger.LogError("No persons found in the database.");

            _logger.LogInfo("Retrieved all persons from the database.");
            var mappedPersons = _mapper.Map<IEnumerable<PersonDto>>(persons);
            return mappedPersons;
        }

        public PersonDto GetPersonByDriverId(int driverId)
        {
            if(driverId <= 0)
            {
                _logger.LogError("Invalid driver ID provided.");
                throw new ArgumentException("Driver ID must be greater than zero.", nameof(driverId));
            }
            var person = _manager.Person.GetPersonByDriverId(driverId);
            if (person is null)
            {
                _logger.LogError($"No person found with driver ID: {driverId}");
                throw new PersonNotFoundException(driverId);
            }
            _logger.LogInfo($"Retrieved person with driver ID: {driverId}");
            var mappedPerson = _mapper.Map<PersonDto>(person);
            return mappedPerson;
        }

        public PersonDto GetPersonById(int id, bool trackChanges)
        {
            if(id <= 0)
            {
                _logger.LogError("Invalid person ID provided.");
                throw new ArgumentException("Person ID must be greater than zero.", nameof(id));
            }
            var person = _manager.Person.GetPersonById(id);
            if (person is null)
            {
                _logger.LogError($"No person found with ID: {id}");
                throw new PersonNotFoundException(id);
            }
            _logger.LogInfo($"Retrieved person with ID: {id}");
            var mappedPerson = _mapper.Map<PersonDto>(person);
            return mappedPerson;
        }

        public PersonDto GetPersonByRegistrationNumber(string registrationNumber)
        {
            if(registrationNumber is null)
            {
                _logger.LogError("Registration number cannot be null.");
                return null;
            }
            var person = _manager.Person.GetPersonByRegistrationNumber(registrationNumber);
            if (person is null)
            {
                _logger.LogError($"No person found with registration number: {registrationNumber}");
                return null;
            }
            _logger.LogInfo($"Retrieved person with registration number: {registrationNumber}");
            var mappedPerson = _mapper.Map<PersonDto>(person);
            return mappedPerson;
        }

        public void SaveOrUpdate(PersonDto person)
        {
            int id = person.Id;
            if(person is null)
            {
                _logger.LogError("Person object sent from controller is null.");
                throw new NullReferenceException("Person object sent from controller is null.");
            }
            if(id <= 0)
            {
                var mappedPerson = _mapper.Map<Person>(person);
                _manager.Person.SaveOrUpdatePerson(mappedPerson);
                _logger.LogInfo("New person created successfully.");
            }
            else if(id > 0)
            {
                var existingPerson = _manager.Person.GetPersonById(id);
                if(existingPerson is null)
                {
                    _logger.LogError($"No person found with ID: {id} for update.");
                    throw new PersonNotFoundException(id);
                }
                var mappedPerson = _mapper.Map<Person>(person);
                _manager.Person.SaveOrUpdatePerson(mappedPerson);
                _logger.LogInfo($"Person with ID: {id} updated successfully.");
            }
            else
            {
                _logger.LogError("Invalid person ID provided.");
                throw new PersonNotFoundException(id);
            }
        }
    }
}
