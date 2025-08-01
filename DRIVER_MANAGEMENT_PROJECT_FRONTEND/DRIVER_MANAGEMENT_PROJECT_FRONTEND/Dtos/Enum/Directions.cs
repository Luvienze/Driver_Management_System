using System.ComponentModel.DataAnnotations;

namespace DRIVER_MANAGEMENT_PROJECT_FRONTEND.Dtos.Enum
{
    public enum Directions
    {
        [Display(Name = "DEPARTURE")]
        DEPARTURE = 0,
        [Display(Name = "ARRIVAL")]
        ARRIVAL = 1
    }
}
