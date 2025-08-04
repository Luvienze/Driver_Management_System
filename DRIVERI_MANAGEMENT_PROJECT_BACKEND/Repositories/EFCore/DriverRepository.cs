using Entities.Enums;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;

namespace Repositories.EFCore
{
    public class DriverRepository : RepositoryBase<Driver>, IDriverRepository
    {
        public DriverRepository(RepositoryContext context) : base(context)
        {
        }

        public IQueryable<Driver> GetActiveDriversByChief(Chief chief, bool isActive) =>
            _context.Drivers
                .Include(d => d.Person)
                .Where(d => d.Chief.Id == chief.Id && d.IsActive == isActive)
                .AsNoTracking();


        public IQueryable<Driver> GetAllDrivers(bool trackChanges) =>
            _context.Drivers
                .Include(d => d.Person)
                .Include(d => d.Chief)
                    .ThenInclude(c => c.Person)
                .Include(d => d.Garage)
                .Where(d => !d.Person.IsDeleted);

        public Driver GetDriverById(int driverId) =>
            FindByCondition(d => d.Id.Equals(driverId), false)
            .SingleOrDefault();
        public Driver GetDriverByRegistrationNumber(string registrationNumber) =>
              _context.Drivers
                  .Include(d => d.Person)
                  .FirstOrDefault(d => d.Person.RegistrationNumber == registrationNumber);

        public int GetDriverGenderCount(Genders gender) =>
            FindByCondition(d => d.Person.Gender.Equals(gender), false)
            .Count();

        public int GetDriverOnDayOff(Days day) =>
            FindByCondition(d => d.DayOff.Equals(day) && d.Person.IsDeleted==false, false)
            .Count();

        public void UpdateDriverByRegistrationNumber(string registrationNumber, Garage garageId, Chief chiefId, CadreTypes cadre, Days dayOff, bool IsActive)
        {
            var driver = FindByCondition(d => d.Person.RegistrationNumber.Equals(registrationNumber), true).SingleOrDefault();
            if (driver != null)
            {
                driver.Garage = garageId;
                driver.Chief = chiefId;
                driver.Cadre = cadre;
                driver.DayOff = dayOff;
                driver.IsActive = IsActive;
                Update(driver);
            }
        }

        public void UpdateIsActive(bool IsActive, int Id)
        {
            var driver = FindByCondition(d => d.Id.Equals(Id), true).SingleOrDefault();
            if (driver != null)
            {
                driver.IsActive = IsActive;
                Update(driver);
            }
        }

        public void SaveOrUpdateDriver(Driver driver)
        {
            if (driver.Id <= 0) 
            {
                Create(driver);
            }
            else
            {
                Update(driver);
            }
        }
    }
}
