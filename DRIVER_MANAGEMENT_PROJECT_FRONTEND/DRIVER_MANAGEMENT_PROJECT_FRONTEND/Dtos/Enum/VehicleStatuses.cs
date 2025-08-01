using System.ComponentModel.DataAnnotations;

namespace DRIVER_MANAGEMENT_PROJECT_FRONTEND.Dtos.Enum
{
    public enum VehicleStatuses
    {
        [Display(Name = "READY FOR SERVICE")]
        READY_FOR_SERVICE = 0,
        [Display(Name = "MALFUNCTION")]
        MALFUNCTION = 1,
        [Display(Name = "DAMAGED")]
        DAMAGED = 2,
        [Display(Name = "OUT OF SERVICE")]
        OUT_OF_SERVICE = 3, 
        [Display(Name = "UNDER MAINTENANCE")]
        UNDER_MAINTENANCE = 4,
    }
}
