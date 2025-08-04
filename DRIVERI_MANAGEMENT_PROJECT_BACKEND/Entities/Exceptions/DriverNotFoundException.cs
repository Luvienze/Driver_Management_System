using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Exceptions
{
    public class DriverNotFoundException : NotFoundException
    {
        public DriverNotFoundException(int driverId)
            : base($"Driver with ID {driverId} not found.")
        {
        }
        public DriverNotFoundException(string registrationNumber)
            : base($"Driver with registration number '{registrationNumber}' not found.")
        {
        }
    }

}
