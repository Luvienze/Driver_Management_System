using System.ComponentModel.DataAnnotations;

namespace DRIVER_MANAGEMENT_PROJECT_FRONTEND.Dto.Enum
{
    public enum Days
    {
        [Display(Name ="MONDAY")]
        MONDAY = 0,
        [Display(Name ="TUESDAY")]
        TUESDAY = 1,
        [Display(Name ="WEDNESDAY")]
        WEDNESDAY = 2,
        [Display(Name ="THURSDAY")]
        THURSDAY = 3,
        [Display(Name ="FRIDAY")]
        FRIDAY = 4,
        [Display(Name ="SATURDAY")]
        SATURDAY = 5,
        [Display(Name ="SUNDAY")]
        SUNDAY = 6
    }
}
