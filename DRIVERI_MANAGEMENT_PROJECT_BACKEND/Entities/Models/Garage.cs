namespace Entities.Models
{
    public class Garage
    {
        public int Id { get; set; }
        public string GarageName { get; set; }
        public string GarageAddress { get; set; }
        public ICollection<Driver> Drivers { get; set; }
        public ICollection<Chief> Chiefs { get; set; } 
        public ICollection<Vehicle> Vehicles { get; set; }
    }
}
