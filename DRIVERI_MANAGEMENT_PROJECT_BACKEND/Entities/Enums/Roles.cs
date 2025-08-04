using System.ComponentModel.DataAnnotations;
namespace Entities.Enums
{
    public enum Roles
    {
        [Display(Name = "ADMIN")]
        ADMIN = 0,
        [Display(Name = "CHIEF")]
        CHIEF = 1,
        [Display(Name = "DRIVER")]
        DRIVER = 2,
    }
}
