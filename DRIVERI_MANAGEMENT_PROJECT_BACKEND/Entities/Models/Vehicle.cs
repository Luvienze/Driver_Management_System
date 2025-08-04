using Entities.Enums;

namespace Entities.Models
{
    public class Vehicle
    {
        public int Id { get; set; }
        public string DoorNo { get; set; }
        public int Capacity { get; set; }
        public int FuelTank { get; set; }
        public string Plate { get; set; }
        public int PersonOnFoot { get; set; }
        public int PersonOnSit { get; set; }
        public bool SuitableForDisabled { get; set; }
        public int ModelYear { get; set; }
        public VehicleStatuses Status { get; set; }
        public int GarageId { get; set; }
        public Garage Garage { get; set; }
    }
}
