using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Exceptions
{
    public sealed class GarageNotFoundException : NotFoundException
    {
        public GarageNotFoundException(int id)
            : base($"Garage with id {id} not found.")
        { }
    }
}
