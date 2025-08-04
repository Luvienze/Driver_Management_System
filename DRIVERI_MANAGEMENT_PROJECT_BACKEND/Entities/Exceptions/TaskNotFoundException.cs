using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Exceptions
{
    public sealed class TaskNotFoundException : NotFoundException
    {
        public TaskNotFoundException(int id)
        : base($"Task with id: {id} doesn't exist in the database.")
        {
        }
    }
}
