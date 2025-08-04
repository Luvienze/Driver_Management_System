using Entities.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface IGarageService
    {
        IEnumerable<GarageDto> GetAllGarages(bool trackChanges);
        GarageDto GetGarageByRegistrationNumber(string registrationNumber);
        GarageDto GetGarageById(GarageDto garageDto);
        void SaveOrUpdateGarage(GarageDto garageDto, bool trackChanges);
        void DeleteOneGarage(int id, bool trackChanges);
    }
}

