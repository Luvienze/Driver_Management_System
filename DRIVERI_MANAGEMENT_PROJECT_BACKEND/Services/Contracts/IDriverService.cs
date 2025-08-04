using Entities.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface IDriverService
    {
        IEnumerable<DriverDto> GetAllDrivers(bool trackChanges);
        IEnumerable<DriverDto> GetActiveDriversByChief(int chiefId, bool isActive);
        DriverDto GetDriverById(int driverId);
        DriverDto GetDriverByRegistrationNumber(string registrationNumber);
        int GetDriverGenderCount(int gender);
        int GetDriverOnDayOff(int day);
        void UpdateDriverByRegistrationNumber(DriverDto driverDto);
        void UpdateIsActive(bool isActive, int id);
        void SaveOrUpdateDriver(DriverDto driverDto);
    }
}
