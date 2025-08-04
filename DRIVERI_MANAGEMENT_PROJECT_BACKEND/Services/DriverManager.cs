using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Enums;
using Entities.Exceptions;
using Entities.Models;
using Repositories.Contracts;
using Services.Contracts;
using System.Security.Cryptography;

namespace Services
{
    public class DriverManager : IDriverService
    {
        private readonly IRepositoryManager _manager;
        private readonly ILoggerService _logger;
        private readonly IMapper _mapper;

        public DriverManager(IRepositoryManager manager, ILoggerService logger, IMapper mapper)
        {
            _manager = manager;
            _logger = logger;
            _mapper = mapper;
        }
        public IEnumerable<DriverDto> GetActiveDriversByChief(int chiefId, bool isActive)
        {
            if(chiefId <= 0) throw new ArgumentException("Chief ID must be greater than zero.", nameof(chiefId));
            var chief = _manager.Chief.GetChiefById(chiefId);
            var drivers = _manager.Driver.GetActiveDriversByChief(chief, isActive);
            if (drivers == null || !drivers.Any())
            {
                _logger.LogInfo($"No active drivers found for chief with ID {chiefId}.");
                return Enumerable.Empty<DriverDto>();
            }
            return drivers.Select(driver => _mapper.Map<DriverDto>(driver)).ToList();
        }

        public IEnumerable<DriverDto> GetAllDrivers(bool trackChanges)
        {
            var drivers = _manager.Driver.GetAllDrivers(trackChanges);
            if(drivers == null || !drivers.Any())
            {
                _logger.LogInfo("No drivers found.");
                return Enumerable.Empty<DriverDto>();
            }
            return drivers.Select(driver => _mapper.Map<DriverDto>(driver)).ToList();
        }

        public DriverDto GetDriverById(int driverId)
        {
            if(driverId <= 0) throw new ArgumentException("Driver ID must be greater than zero.", nameof(driverId));
            var driver = _manager.Driver.GetDriverById(driverId);
            if (driver == null)
            {
                _logger.LogInfo($"Driver with ID {driverId} not found.");
                throw new DriverNotFoundException(driverId);
            }
            return _mapper.Map<DriverDto>(driver);
        }

        public DriverDto GetDriverByRegistrationNumber(string registrationNumber)
        {
            if(registrationNumber is null) throw new ArgumentNullException(nameof(registrationNumber), "Registration number cannot be null.");
            var driver = _manager.Driver.GetDriverByRegistrationNumber(registrationNumber);
            if (driver == null)
            {
                _logger.LogInfo($"Driver with registration number '{registrationNumber}' not found.");
                throw new DriverNotFoundException(registrationNumber);
            }
            return _mapper.Map<DriverDto>(driver);
        }

        public int GetDriverGenderCount(int gender)
        {
            if (!Enum.IsDefined(typeof(Genders), gender))
                throw new ArgumentOutOfRangeException(nameof(gender), "Invalid gender value.");

            var genderEnum = (Genders)gender;
            int count = _manager.Driver.GetDriverGenderCount(genderEnum);
            if(count < 0)
            {
                _logger.LogInfo($"Driver with {gender} not found.");
                return count;
            }
            return count;
        }

        public int GetDriverOnDayOff(int day)
        {
            if (day < 0 || day > 6)
                throw new ArgumentOutOfRangeException(nameof(day), "Day must be between 0 (Sunday) and 6 (Saturday).");
            var dayEnum = (Days)day;
            int count = _manager.Driver.GetDriverOnDayOff(dayEnum);
            if(count < 0)
            {
                _logger.LogInfo($"No drivers found on day {day}.");
                return count;
            }
            return count;
        }

        public void SaveOrUpdateDriver(DriverDto driverDto)
        {
            int id = driverDto.Id;
            if(driverDto is null) throw new ArgumentNullException(nameof(driverDto), "Driver DTO cannot be null.");
            if(driverDto.Id < 0)
            {
                _manager.Driver.SaveOrUpdateDriver(_mapper.Map<Driver>(driverDto));
                _logger.LogInfo($"Driver with registration number '{driverDto.RegistrationNumber}' created successfully.");
                _manager.Save();
            }
            else if (id > 0)
            {
                var existingDriver = _manager.Driver.GetDriverById(id);
                if (existingDriver == null)
                {
                    _logger.LogInfo($"Driver with ID {id} not found for update.");
                    throw new DriverNotFoundException(id);
                }
                _manager.Driver.SaveOrUpdateDriver(_mapper.Map<Driver>(driverDto));
                _logger.LogInfo($"Driver with ID {id} updated successfully.");
                _manager.Save();
            }
            else
            {
                throw new DriverNotFoundException(id);
            }
        }

        public void UpdateDriverByRegistrationNumber(DriverDto driverDto)
        {
            string registrationNumber = driverDto.RegistrationNumber;
            if(string.IsNullOrWhiteSpace(registrationNumber)) 
                throw new ArgumentException("Registration number cannot be null or empty.", nameof(registrationNumber));
            var driver = _manager.Driver.GetDriverByRegistrationNumber(registrationNumber);

            if (driver == null)
            {
                _logger.LogInfo($"Driver with registration number '{registrationNumber}' not found for update.");
                throw new DriverNotFoundException(registrationNumber);
            }

            var garage = _manager.Garage.GetGarageByGarageName(driverDto.Garage, false);
            var chief = _manager.Chief.GetChiefById(driverDto.ChiefId);
            var cadreEnum = (CadreTypes)driverDto.Cadre; 
            var dayOffEnum = (Days)driverDto.DayOff;
            var isActive = driverDto.IsActive;
            _manager.Driver.UpdateDriverByRegistrationNumber(registrationNumber, garage, chief, cadreEnum, dayOffEnum, isActive);
            _manager.Save();
        }

        public void UpdateIsActive(bool isActive, int id)
        {
            if(id < 0) throw new ArgumentException("Driver ID must be greater than or equal to zero.", nameof(id));

            var driver = _manager.Driver.GetDriverById(id);
            if (driver == null)
            {
                _logger.LogInfo($"Driver with ID {id} not found for update.");
                throw new DriverNotFoundException(id);
            }
            _manager.Driver.UpdateIsActive(isActive, id);
            _logger.LogInfo($"Driver with ID {id} updated successfully to active status: {isActive}.");
            _manager.Save();
        }
    }
}
