using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DRIVER_MANAGEMENT_PROJECT_FRONTEND.Dto.Enum
{
    public enum Tasks
    {
        [Display(Name = "COMPLETED")]
        COMPLETED = 0,
        [Display(Name = "IN PROGRESS")]
        IN_PROGRESS = 1,
        [Display(Name = "CANCELLED")]
        CANCELLED = 2, 
        [Display(Name = "PLANNED")]
        PLANNED = 3
    }
}
