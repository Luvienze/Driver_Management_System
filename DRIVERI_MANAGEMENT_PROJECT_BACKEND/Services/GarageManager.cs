using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Exceptions;
using Entities.Models;
using Repositories.Contracts;
using Services.Contracts;

namespace Services
{
    public class GarageManager : IGarageService
    {
        private readonly IRepositoryManager _manager;
        private readonly ILoggerService _logger;
        private readonly IMapper _mapper;

        public GarageManager(IRepositoryManager manager, ILoggerService logger, IMapper mapper)
        {
            _manager = manager;
            _logger = logger;
            _mapper = mapper;
        }

        public void DeleteOneGarage(int id, bool trackChanges)
        {
            if(id < 0) throw new ArgumentException("Garage ID must be greater than or equal to zero.", nameof(id));

            var garage = _manager.Garage.GetGarageById(id, trackChanges);
            if (garage is null)
            {
                string message = $"Garage with id {id} not found.";
                _logger.LogInfo(message);
                throw new GarageNotFoundException(id);
            }
            _manager.Garage.DeleteGarage(garage);
            _manager.Save();
        }

        public IEnumerable<GarageDto> GetAllGarages(bool trackChanges)
        {
            var garages = _manager.Garage.GetAllGarages(trackChanges) ?? Enumerable.Empty<Garage>();
            if (!garages.Any())
            {
                _logger.LogInfo("No garages found.");
                return Enumerable.Empty<GarageDto>();
            }
            return garages.Select(garage => _mapper.Map<GarageDto>(garage)).ToList();
        }

        public GarageDto GetGarageById(GarageDto garageDto)
        {
            if (garageDto == null) throw new ArgumentNullException(nameof(garageDto), "Garage DTO cannot be null.");
            var selectedGarage = _manager.Garage.GetGarageById(garageDto.Id, false);
            if (selectedGarage == null)
            {
                string message = $"Garage with id {garageDto.Id} not found.";
                _logger.LogInfo(message);
                throw new GarageNotFoundException(garageDto.Id);
            }
            return _mapper.Map<GarageDto>(selectedGarage);
        }

        public GarageDto GetGarageByRegistrationNumber(string registrationNumber)
        {
            if(registrationNumber is null) throw new ArgumentNullException(nameof(registrationNumber), "Registration number cannot be null.");
            
            var garage = _manager.Garage.GetGarageByRegistrationNumber(registrationNumber, false);
            if (garage == null)
            {
                string message = $"Garage with registration number {registrationNumber} not found.";
                _logger.LogInfo(message);
                throw new GarageNotFoundException(0);
            }
            return _mapper.Map<GarageDto>(garage);
        }

        public void SaveOrUpdateGarage(GarageDto garageDto, bool trackChanges)
        {
            if (garageDto == null)
                throw new ArgumentNullException(nameof(garageDto), "Garage DTO cannot be null.");

            if (garageDto.Id < 0)
            {
                var garage = _mapper.Map<Garage>(garageDto);
                _manager.Garage.SaveOrUpdateGarage(garage);
            }
            else
            {
                var existingGarage = _manager.Garage.GetGarageById(garageDto.Id, trackChanges);
                if (existingGarage == null)
                {
                    string message = $"Garage with id {garageDto.Id} not found.";
                    _logger.LogInfo(message);
                    throw new GarageNotFoundException(garageDto.Id);
                }

                _mapper.Map(garageDto, existingGarage);
            }

            _manager.Save();
        }
    }
}
