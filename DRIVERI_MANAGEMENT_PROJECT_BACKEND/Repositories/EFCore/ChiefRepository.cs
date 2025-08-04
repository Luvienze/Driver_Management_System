using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.EFCore
{
    public class ChiefRepository : RepositoryBase<Chief>, IChiefRepository
    {
        public ChiefRepository(RepositoryContext context) : base(context)
        {
        }

        public IQueryable<Chief> GetActiveChiefs() =>
            FindByCondition(c => c.IsActive == true, false);

        public IQueryable<Chief> GetAllChief(bool trackChanges) =>
            FindAll(trackChanges);

        public Chief GetChiefById(int chiefId) =>
            FindByCondition(C => C.Id.Equals(chiefId), false)
            .SingleOrDefault();

        public Chief GetChiefByName(string fullName) =>
           FindByCondition( c => string.Equals(
            (c.Person.FirstName.Trim() + " " + c.Person.LastName.Trim()),
            fullName.Trim(),
            StringComparison.OrdinalIgnoreCase), false).SingleOrDefault();

        public Chief GetChiefByRegistrationNumber(string registrationNumber) =>
            FindByCondition(c => c.Person.RegistrationNumber.Equals(registrationNumber), false)
            .SingleOrDefault();

        public Chief GetPersonChiefByRegistrationNumber(string registrationNumber)
        {
            var driver = _context.Drivers
                .Include(d => d.Person)
                .Include(d => d.Chief)
                    .ThenInclude(c => c.Person)
                .Include(d => d.Chief)
                    .ThenInclude(c => c.Garage)
                .FirstOrDefault(d => d.Person.RegistrationNumber == registrationNumber);

            if (driver == null || driver.Chief == null)
                return null;

            return driver.Chief;
        }

        public void CreateChief(Chief chief) => Create(chief);
        public void UpdateChief(Chief chief) => Update(chief);
        public void DeleteChief(Chief chief) => Delete(chief);
    }
}
