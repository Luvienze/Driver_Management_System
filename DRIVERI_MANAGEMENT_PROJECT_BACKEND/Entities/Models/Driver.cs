using Entities.Enums;
namespace Entities.Models
{
    public class Driver
    {
        public int Id { get; set; }
        public int PersonId { get; set; }
        public Person Person{ get; set; }
        public int GarageId { get; set; }
        public Garage Garage { get; set; }
        public int ChiefId { get; set; }
        public Chief Chief { get; set; }
        public CadreTypes Cadre { get; set; }
        public Days DayOff { get; set; }
        public bool IsActive { get; set; }
    }
}
