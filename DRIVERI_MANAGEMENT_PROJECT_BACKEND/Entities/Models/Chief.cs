using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    public class Chief
    {
        public int Id { get; set; }
        public int PersonId { get; set; }
        public Person Person{ get; set; }
        public int GarageId { get; set; }
        public Garage Garage { get; set; }
        public bool IsActive { get; set; }
        public ICollection<Driver> Drivers { get; set; }

    }
}
