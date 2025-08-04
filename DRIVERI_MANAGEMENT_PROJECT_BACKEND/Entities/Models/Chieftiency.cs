using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Chieftiency
    {
        public int Id { get; set; }
        public string ChieftiencyName { get; set; }
        public int GarageId { get; set; }
        public Garage Garage{ get; set; }
    }
}
