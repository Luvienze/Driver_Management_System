using System.ComponentModel.DataAnnotations;

namespace DRIVER_MANAGEMENT_PROJECT_FRONTEND.Dto.Enum
{
    public enum Genders
    {
        [Display(Name ="MALE")]
        MALE = 0,
        [Display(Name ="FEMALE")]
        FEMALE = 1,
        [Display(Name ="OTHER")]
        OTHER = 2
    }
}
