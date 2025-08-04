using System.ComponentModel.DataAnnotations;
namespace Entities.Enums
{
    public enum CadreTypes
    {
        [Display(Name = "Urban Public Transport")]
        URBAN_PUBLIC_TRANSPORT = 0,
        [Display(Name = "Intercity Transport")]
        INTERCITY_TRANSPORT = 1,
        [Display(Name = "Corporate Shuttle Service")]
        CORPORATE_SHUTTLE_SERVICE = 2,
        [Display(Name = "Tourism and Charter Service")]
        TOURISM_AND_CHARTER_SERVICE = 3,
        [Display(Name = "Night Shift Operation")]
        NIGHT_SHIFT_OPERATION = 4,
        [Display(Name = "Backup and Relief Staff Transport")]
        BACKUP_AND_RELIEF_STAFF = 5,
        [Display(Name = "Training and Evaluation Transport")]
        TRAINING_AND_EVALUATION = 6,
        [Display(Name = "VIP and Private Transport")]
        VIP_AND_PRIVATE_TRANSPORT = 7,
        [Display(Name ="Municipal Staff Transport")]
        MUNICIPAL_STAFF_TRANSPORT = 8,
        [Display(Name = "Aiport Logistic Transport")]
        AIRPORT_LOGISTIC_TRANSFER = 9
    }
}
