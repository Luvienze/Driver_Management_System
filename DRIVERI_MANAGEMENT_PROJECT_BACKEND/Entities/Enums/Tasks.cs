using System.ComponentModel.DataAnnotations;
namespace Entities.Enums
{
    public enum Tasks
    {
        [Display(Name ="Completed")]
        COMPLETED = 0,
        [Display(Name = "In Progress")]
        IN_PROGRESS = 1,
        [Display(Name = "Cancelled")]
        CANCELLED = 2,
        [Display(Name = "Planned")]
        PLANNED = 3,
    }
}
