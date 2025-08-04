using Entities.DataTransferObjects;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface IRoleService
    {
        IEnumerable<RoleDto> GetAllRoles(bool trackChanges);
        IEnumerable<RoleDto> GetRolesByRegistrationNumberAndPhone(string registrationNumber, string phoneNumber);
        IEnumerable<RoleDto> GetRoleByPersonId(int personId, bool trackChanges);
        void SaveOrUpdateRole(RoleDto role);
        void DeleteOneRole(int id, bool trackChanges);
    }
}
