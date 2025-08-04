using System.ComponentModel.DataAnnotations;
namespace Entities.Enums
{
    public enum Directions
    {
        [Display(Name = "Departure")]
        DEPARTURE = 0,
        [Display(Name = "Arrival")]
        ARRIVAL = 1
    }
}
