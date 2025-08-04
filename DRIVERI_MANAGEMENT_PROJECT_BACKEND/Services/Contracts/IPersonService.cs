using Entities.DataTransferObjects;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface IPersonService
    {
        IEnumerable<PersonDto> GetAllPersons(bool trackChanges);
        PersonDto GetPersonById(int id, bool trackChanges);
        PersonDto GetPersonByDriverId(int driverId);
        PersonDto GetPersonByRegistrationNumber(string registrationNumber);
        void SaveOrUpdate(PersonDto person); 
        void DeletePerson(PersonDto person);  
    }
}
