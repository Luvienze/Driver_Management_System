using Entities.Enums;
using Entities.Models;

namespace Repositories.Contracts
{
    public interface IVehicleRepository : IRepositoryBase<Vehicle>
    {
        IQueryable<Vehicle> GetVehicles(bool trackChanges);
        IQueryable<Vehicle> GetVehicleListByGarageId(int garageId, VehicleStatuses status);
        Vehicle GetVehicleById(int vehicleId, bool trackChanges);
        Vehicle GetVehicleByDoorNo(string doorNo);
        Vehicle GetVehicleByGarageId(int garageId, VehicleStatuses status);
        int GetVehicleCountByStatus(VehicleStatuses status);
        void SaveOrUpdateVehicle(Vehicle vehicle);
        void DeleteVehicle(Vehicle vehicle);
    }
}
