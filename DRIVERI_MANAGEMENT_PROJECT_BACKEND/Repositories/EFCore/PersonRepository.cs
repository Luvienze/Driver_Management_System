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
    public class PersonRepository : RepositoryBase<Person>, IPersonRepository
    {
        public PersonRepository(RepositoryContext context) : base(context)
        {

        }
        public void SaveOrUpdatePerson(Person person)
        {
            if (person.Id <= 0)
                Create(person);
            else
                Update(person);
        }

        public void DeletePerson(Person person)
        {
            person.IsDeleted = true;
            Update(person);
        }

        public IQueryable<Person> GetAllPersons(bool trackChanges) =>
            FindAll(trackChanges);

        public Person GetPersonByDriverId(int driverId)
        {
            var driver = _context.Drivers
                                 .Include(d => d.Person)
                                 .FirstOrDefault(d => d.Id == driverId);
            return driver?.Person;
        }

        public Person GetPersonById(int personId) =>
            FindByCondition(p => p.Id.Equals(personId), false).SingleOrDefault();

        public Person GetPersonByRegistrationNumber(string registrationNumber) 
             {
            var driver = _context.Drivers
                                 .AsNoTracking()
                                 .Include(d => d.Person)
                                 .FirstOrDefault(d => d.Person.RegistrationNumber.Equals(registrationNumber));
            return driver?.Person;
        }

}
}
