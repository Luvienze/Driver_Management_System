using Entities.DataTransferObjects;

namespace Services.Contracts
{
    public interface IVehicleService
    {
        IEnumerable<VehicleDto> GetAllVehicles(bool trackChanges);
        IEnumerable<VehicleDto> GetVehicleListByGarageId(int garageId);
        VehicleDto GetVehicleByDoorNo(string doorNo);
        VehicleDto GetVehicleById(int id, bool trackChanges);
        VehicleDto GetVehicleByGarageId(int garageId, int status);
        int GetVehicleCountByStatus(int status);
        void SaveOrUpdateVehicle(VehicleDto vehicleDto);
        void DeleteOneVehicle(int id, bool trackChanges);
    }
}
