using System.ComponentModel.DataAnnotations;
namespace DRIVER_MANAGEMENT_PROJECT_FRONTEND.Dto.Enum

{
    public enum BloodGroups
    {
        [Display(Name = "A+")]
        A_POSITIVE = 0,

        [Display(Name = "A-")]
        A_NEGATIVE = 1,

        [Display(Name = "B+")]
        B_POSITIVE = 2,

        [Display(Name = "B-")]
        B_NEGATIVE = 3,

        [Display(Name = "AB+")]
        AB_POSITIVE = 4,

        [Display(Name = "AB-")]
        AB_NEGATIVE = 5,

        [Display(Name = "O+")]
        O_POSITIVE = 6,

        [Display(Name = "O-")]
        O_NEGATIVE = 7 
    }
}