using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;
using System.Linq;

namespace Repositories.EFCore
{
    public class GarageRepository : RepositoryBase<Garage>, IGarageRepository
    {
        public GarageRepository(RepositoryContext context) : base(context)
        {
        }

        public void SaveOrUpdateGarage(Garage garage) => Create(garage);

        public void DeleteGarage(Garage garage) => Delete(garage);

        public IQueryable<Garage> GetAllGarages(bool trackChanges) =>
            FindAll(trackChanges);

        public Garage GetGarageByGarageName(string garageName, bool trackChanges) =>
            FindByCondition(g => g.GarageName.Equals(garageName), trackChanges)
            .SingleOrDefault();

        public Garage GetGarageById(int garageId, bool trackChanges) =>
            FindByCondition(g => g.Id.Equals(garageId), trackChanges)
            .SingleOrDefault();

        public Garage GetGarageByRegistrationNumber(string registrationNumber, bool trackChanges)
        {
            var driver = _context.Drivers
                .Include(d => d.Person)
                .Include(d => d.Chief)
                .ThenInclude(c => c.Garage)
                .FirstOrDefault(d => d.Person.RegistrationNumber == registrationNumber);

            return driver?.Chief?.Garage;
        }
    }
}
