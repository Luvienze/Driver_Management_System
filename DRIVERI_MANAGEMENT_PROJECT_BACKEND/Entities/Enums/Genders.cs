using System.ComponentModel.DataAnnotations;
namespace Entities.Enums
{
    public enum Genders
    {
        [Display(Name = "Male")]
        MALE = 0,
        [Display(Name = "Female")]
        FEMALE = 1,
        [Display(Name = "Other")]
        OTHER = 2
    }
}
