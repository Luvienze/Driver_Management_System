using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Exceptions
{
    public sealed class RoleNotFoundException : NotFoundException
    {
        public RoleNotFoundException(int id) 
            : base($"Role with id: {id} doesn't exist in the database.")
        {
        }
        public RoleNotFoundException(string registrationNumber, string phoneNumber)
            : base($"Role with registration number: {registrationNumber} and phone number: {phoneNumber} doesn't exist in the database.")
        {
        }
    }
}
