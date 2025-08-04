using Entities.Models;

namespace Repositories.Contracts
{
    public interface IPersonRepository : IRepositoryBase<Person>
    {
        IQueryable<Person> GetAllPersons(bool trackChanges);

        Person GetPersonById(int personId);

        Person GetPersonByDriverId(int driverId);

        Person GetPersonByRegistrationNumber(string registrationNumber);

        void SaveOrUpdatePerson(Person person);

        void DeletePerson(Person person);
    }
}
