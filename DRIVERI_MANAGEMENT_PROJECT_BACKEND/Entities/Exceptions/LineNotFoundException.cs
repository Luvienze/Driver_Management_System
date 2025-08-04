using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Exceptions
{
    public class LineNotFoundException : NotFoundException
    {
        public LineNotFoundException(int id)
         : base($"Line with id: {id} doesn't exist in the database.")
        {
        }
    }
}
