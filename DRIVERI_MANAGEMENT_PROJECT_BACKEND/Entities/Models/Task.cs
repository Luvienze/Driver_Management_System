using Entities.Enums;

namespace Entities.Models
{
    public class Task
    {
        public int Id { get; set; }
        public int DriverId { get; set; }
        public Driver Driver { get; set; }
        public int VehicleId { get; set; }
        public Vehicle Vehicle { get; set; }
        public int RouteId { get; set; }
        public Route Route { get; set; }
        public int LineCodeId { get; set; }
        public Line LineCode { get; set; }
        public Directions Direction { get; set; }
        public DateTime DateOfStart { get; set; }
        public DateTime DateOfEnd { get; set; }
        public int PassengerCount { get; set; }
        public DateTime OrerStart { get; set; }
        public DateTime OrerEnd { get; set; }
        public DateTime ChiefStart { get; set; }
        public DateTime ChiefEnd { get; set; }
        public Tasks Status{ get; set; }
    }
}
