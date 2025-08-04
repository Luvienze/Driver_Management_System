using Entities.Models;

namespace Repositories.Contracts
{
    public interface IGarageRepository : IRepositoryBase<Garage>
    {
        IQueryable<Garage> GetAllGarages(bool trackChanges);
        Garage GetGarageById(int garageId, bool trackChanges);
        Garage GetGarageByGarageName(string garageName, bool trackChanges);
        Garage GetGarageByRegistrationNumber(string registrationNumber, bool trackChanges);
        void SaveOrUpdateGarage(Garage garage);
        void DeleteGarage(Garage garage);
    }
}
