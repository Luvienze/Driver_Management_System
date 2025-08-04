using System.ComponentModel.DataAnnotations;
namespace Entities.Enums
{
    public enum BloodGroups
    {
        [Display(Name ="A+")]
        A_POSITIVE = 1,
        [Display(Name = "A-")]
        A_NEGATIVE = 2,
        [Display(Name = "B+")]
        B_POSITIVE = 3,
        [Display(Name = "B-")]
        B_NEGATIVE = 4,
        [Display(Name = "AB+")]
        AB_POSITIVE = 5,
        [Display(Name = "AB-")]
        AB_NEGATIVE = 6,
        [Display(Name = "O+")]
        O_POSITIVE = 7,
        [Display(Name = "O-")]
        O_NEGATIVE = 8
    }
}
