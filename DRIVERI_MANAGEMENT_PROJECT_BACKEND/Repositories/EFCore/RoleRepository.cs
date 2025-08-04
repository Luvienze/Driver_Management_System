using Entities.Models;
using Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.EFCore
{
    public class RoleRepository : RepositoryBase<Role>, IRoleRepository
    {
        public RoleRepository(RepositoryContext context) : base(context)
        {

        }

        public void SaveOrUpdateRole(Role role)
        {
            if (role.Id <= 0)
                Create(role);
            else
                Update(role);
        }
        public void DeleteRole(Role role) => Delete(role);

        public IQueryable<Role> FindRoleByPersonId(int personId, bool trackChanges) =>
            FindByCondition(r => r.PersonId.Equals(personId), trackChanges);

        public IQueryable<Role> FindRolesByRegistrationAndPhone(string registrationNumber, string phoneNumber) =>
            FindByCondition(r => r.Person.RegistrationNumber.Equals(registrationNumber) &&
                                 r.Person.Phone.Equals(phoneNumber)
            , trackChanges: false);

        public IQueryable<Role> GetAllRoles(bool trackChanges) =>
            FindAll(trackChanges);

        public Role GetRoleById(int roleId, bool trackChanges) =>
            FindByCondition(r => r.Id.Equals(roleId), trackChanges)
            .SingleOrDefault();

    }
}
