using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Exceptions
{
    public sealed class RouteNotFoundException : NotFoundException
    {
        public RouteNotFoundException(int id)
            : base($"Route with id {id} not found.") { }
        
    }
}
