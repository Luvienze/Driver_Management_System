using System.ComponentModel.DataAnnotations;
namespace Entities.Enums
{
    public enum VehicleStatuses
    {
        [Display(Name ="Ready For Service")]
        READY_FOR_SERVICE = 0,
        [Display(Name = "Malfunction")]
        MALFUNCTION = 1,
        [Display(Name = "Damaged")]
        DAMAGED = 2,
        [Display(Name = "Out of Service")]
        OUT_OF_SERVICE = 3,
        [Display(Name = "Under Maintenance")]
        UNDER_MAINTENANCE = 4,
    }
}
