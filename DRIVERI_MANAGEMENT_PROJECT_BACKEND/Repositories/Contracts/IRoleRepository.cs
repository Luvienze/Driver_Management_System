using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Contracts
{
    public interface IRoleRepository : IRepositoryBase<Role>
    {
        IQueryable<Role> GetAllRoles(bool trackChanges);
        IQueryable<Role> FindRolesByRegistrationAndPhone(string registrationNumber, string phoneNumber);
        IQueryable<Role> FindRoleByPersonId(int personId, bool trackChanges);
        Role GetRoleById(int roleId, bool trackChanges);
        void SaveOrUpdateRole(Role role);
        void DeleteRole(Role role);
    }

}
