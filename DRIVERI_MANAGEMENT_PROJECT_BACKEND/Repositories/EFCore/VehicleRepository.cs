using Entities.Enums;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;

namespace Repositories.EFCore
{
    public class VehicleRepository : RepositoryBase<Vehicle>, IVehicleRepository
    {
        public VehicleRepository(RepositoryContext context) : base(context)
        {
        }

        public void SaveOrUpdateVehicle(Vehicle vehicle) => Create(vehicle);
        public void DeleteVehicle(Vehicle vehicle) => Delete(vehicle);

        public Vehicle GetVehicleByDoorNo(string doorNo) =>
          _context.Vehicles
              .Include(v => v.Garage)
              .FirstOrDefault(v => v.DoorNo.Equals(doorNo));

        public Vehicle GetVehicleByGarageId(int garageId, VehicleStatuses status) =>
             _context.Vehicles
                 .Include(v => v.Garage)
                 .SingleOrDefault(v => v.GarageId == garageId && v.Status == status);

        public IQueryable<Vehicle> GetVehicleListByGarageId(int garageId, VehicleStatuses status) =>
             _context.Vehicles
                 .Include(v => v.Garage)
                 .Where(v => v.GarageId == garageId && v.Status == status)
                 .AsNoTracking();

        public Vehicle GetVehicleById(int vehicleId, bool trackChanges) =>
            FindByCondition(e => e.Id.Equals(vehicleId), trackChanges)
            .SingleOrDefault();

        public int GetVehicleCountByStatus(VehicleStatuses status) =>
            FindByCondition(e => e.Status.Equals(status), false)
            .Count();

        public IQueryable<Vehicle> GetVehicles(bool trackChanges) =>
            FindAll(trackChanges);

    }
}
