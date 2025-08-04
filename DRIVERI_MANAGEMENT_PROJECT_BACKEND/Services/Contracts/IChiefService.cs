using Entities.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface IChiefService
    {
        IEnumerable<ChiefDto> GetAllChiefs(bool trackChanges);
        IEnumerable<ChiefDto> GetActiveChiefs();
        ChiefDto GetChiefByRegistrationNumber(string registrationNumber);
        ChiefDto GetChiefByName(string fullname);
        ChiefDto GetPersonChiefByRegistrationNumber(string registrationNumber);
        ChiefDto GetChiefById(int chiefId);
        void UpdateOneChief(int id, ChiefDto chiefDto, bool trackChanges);
        void DeleteOneChief(int id, bool trackChanges);
    }
}
