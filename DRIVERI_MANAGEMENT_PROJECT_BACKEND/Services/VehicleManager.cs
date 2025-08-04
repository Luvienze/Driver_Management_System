using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Enums;
using Entities.Exceptions;
using Entities.Models;
using Repositories.Contracts;
using Services.Contracts;

namespace Services
{
    public class VehicleManager : IVehicleService
    {
        private readonly IRepositoryManager _manager;
        private readonly ILoggerService _logger;
        private readonly IMapper _mapper;

        public VehicleManager(IRepositoryManager manager, ILoggerService logger, IMapper mapper)
        {
            _manager = manager;
            _logger = logger;
            _mapper = mapper;
        }
        public void SaveOrUpdateVehicle(VehicleDto vehicleDto)
        {
            if(vehicleDto == null) throw new ArgumentNullException(nameof(vehicleDto)); 
            int id = vehicleDto.Id;
            if (id <= 0) 
            { 
                var mappedVehicle = _mapper.Map<Vehicle>(vehicleDto);
                _manager.Vehicle.SaveOrUpdateVehicle(mappedVehicle);
                _manager.Save();
            }else if (id > 0) 
            {
                var existingVehicle = _manager.Vehicle.GetVehicleById(id, false);
                if (existingVehicle == null) throw new VehicleNotFoundException(id);
                existingVehicle = _mapper.Map<Vehicle>(vehicleDto);
                _manager.Vehicle.SaveOrUpdateVehicle(existingVehicle);
                _manager.Save();
            }
        }

        public void UpdateOneVehicle(int id, VehicleDto vehicleDto, bool trackChanges)
        {
            if (vehicleDto is null) throw new ArgumentNullException(nameof(vehicleDto), "VehicleDto cannot be null.");
            var vehicle = _manager.Vehicle.GetVehicleById(id, trackChanges);
            if (vehicle == null)
            {
                _logger.LogInfo($"Vehicle with id {id} not found.");
                throw new KeyNotFoundException($"Vehicle with id {id} not found.");
            }
            _mapper.Map(vehicleDto, vehicle);
            _manager.Vehicle.Update(vehicle);
            _manager.Save();
        }

        public void DeleteOneVehicle(int id, bool trackChanges)
        {
            if(id < 0) throw new ArgumentException("Vehicle ID must be greater than or equal to zero.", nameof(id));
            var vehicle = _manager.Vehicle.GetVehicleById(id, trackChanges);
            if (vehicle == null)
            {
                _logger.LogInfo($"Vehicle with id {id} not found.");
                throw new KeyNotFoundException($"Vehicle with id {id} not found.");
            }
            _manager.Vehicle.Delete(vehicle);
            _manager.Save();
        }

        public IEnumerable<VehicleDto> GetAllVehicles(bool trackChanges)
        {
            var vehicles = _manager.Vehicle.GetVehicles(trackChanges);
            if (!vehicles.Any()) {
                _logger.LogInfo("No vehicles found.");
                return Enumerable.Empty<VehicleDto>();
            }
            return vehicles.Select(vehicle => _mapper.Map<VehicleDto>(vehicle)).ToList();
        }

        public VehicleDto GetVehicleByDoorNo(string doorNo)
        {
            if (doorNo is null) throw new ArgumentNullException(nameof(doorNo), "Door number cannot be null.");
            var vehicle = _manager.Vehicle.GetVehicleByDoorNo(doorNo);
            if (vehicle == null)
            {
                _logger.LogInfo($"Vehicle with door number {doorNo} not found.");
                throw new VehicleNotFoundException(vehicle.Id);

            }
            return _mapper.Map<VehicleDto>(vehicle);
        }

        public VehicleDto GetVehicleByGarageId(int garageId, int status)
        {
            if (garageId < 0) throw new ArgumentException("Garage ID must be greater than or equal to zero.", nameof(garageId));
            if (status < 0) throw new ArgumentNullException(nameof(status), "Status cannot be null.");
            if (!Enum.IsDefined(typeof(VehicleStatuses), status))
            {
                _logger.LogInfo($"Invalid status value: {status}.");
                throw new ArgumentException("Invalid status value.", nameof(status));
            }
            var vehicleStatus = (VehicleStatuses)status;
            var vehicle = _manager.Vehicle.GetVehicleByGarageId(garageId, vehicleStatus);
            if (vehicle == null)
            {
                _logger.LogInfo($"Vehicle with garage ID {garageId} and status {status} not found.");
                throw new VehicleNotFoundException(vehicle.Id);
            }
            return _mapper.Map<VehicleDto>(vehicle);
        }

        public VehicleDto GetVehicleById(int id, bool trackChanges)
        {
            if(id <0 ) throw new ArgumentException("Vehicle ID must be greater than or equal to zero.", nameof(id));
            var vehicle = _manager.Vehicle.GetVehicleById(id, trackChanges);
            if (vehicle == null)
            {
                _logger.LogInfo($"Vehicle with id {id} not found.");
                throw new VehicleNotFoundException(id);
            }
            return _mapper.Map<VehicleDto>(vehicle);
        }

        public int GetVehicleCountByStatus(int status)
        {
            if (status < 0) throw new ArgumentException("Status must be greater than or equal to zero.", nameof(status));
            if (!Enum.IsDefined(typeof(VehicleStatuses), status))
                throw new ArgumentException("Invalid status value.", nameof(status));

            var vehicleStatus = (VehicleStatuses)status;
            var count = _manager.Vehicle.GetVehicleCountByStatus(vehicleStatus);
            if (count < 0)
            {
                _logger.LogInfo($"No vehicles found with status {status}.");
                return 0;
            }
            return count;

        }
        public IEnumerable<VehicleDto> GetVehicleListByGarageId(int garageId)
        {
            if (garageId <= 0) throw new GarageNotFoundException(garageId);
            var vehicles = _manager.Vehicle.GetVehicleListByGarageId(garageId, VehicleStatuses.MALFUNCTION);
            if (!vehicles.Any()) throw new VehicleNotFoundException(garageId);
            var mappedVehicles = vehicles.Select(vehicle => _mapper.Map<VehicleDto>(vehicle)).ToList();
            return mappedVehicles;
        }
    }
}
