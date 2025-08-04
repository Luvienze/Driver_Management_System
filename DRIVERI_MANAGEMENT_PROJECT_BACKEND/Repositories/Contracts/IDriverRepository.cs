using Entities.Enums;
using Entities.Models;

namespace Repositories.Contracts
{
    public interface IDriverRepository :IRepositoryBase<Driver>
    {
        IQueryable<Driver> GetAllDrivers(bool trackChanges);
        IQueryable<Driver> GetActiveDriversByChief(Chief chief, bool isActive);
        void UpdateIsActive(bool IsActive, int Id);
        void UpdateDriverByRegistrationNumber(string registrationNumber,
            Garage garageId, Chief chiefId, CadreTypes  cadre, Days dayOff, bool IsActive);
        void SaveOrUpdateDriver(Driver driver);
        int GetDriverOnDayOff(Days day);
        int GetDriverGenderCount(Genders gender);
        Driver GetDriverById(int driverId);
        Driver GetDriverByRegistrationNumber(string registrationNumber);
    }
}
