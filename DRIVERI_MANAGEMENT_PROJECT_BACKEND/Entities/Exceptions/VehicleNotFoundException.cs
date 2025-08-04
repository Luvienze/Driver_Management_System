using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Exceptions
{
    public sealed class VehicleNotFoundException : NotFoundException
    {
        public VehicleNotFoundException(int id)
             : base($"Vehicle with id {id} not found.")
        { }
    }
}
