using System.ComponentModel.DataAnnotations;
namespace Entities.Enums
{
    public enum Days
    {
        [Display(Name = "Monday")]
        MONDAY = 0,
        [Display(Name = "Tuesday")]
        TUESDAY = 1,
        [Display(Name = "Wednesday")]
        WEDNESDAY = 2,
        [Display(Name = "Thursday")]
        THURSDAY = 3,
        [Display(Name = "Friday")]
        FRIDAY = 4,
        [Display(Name = "Saturday")]
        SATURDAY = 5,
        [Display(Name = "Sunday")]
        SUNDAY = 6
    }
}
