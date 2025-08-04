using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Contracts
{
    public interface IChiefRepository : IRepositoryBase<Chief>
    {
        IQueryable<Chief> GetAllChief(bool trackChanges);
        IQueryable<Chief> GetActiveChiefs();
        Chief GetChiefById(int chiefId);
        Chief GetChiefByName(string fullName);
        Chief GetChiefByRegistrationNumber(string registrationNumber);
        Chief GetPersonChiefByRegistrationNumber(string registrationNumber);
    }
}
