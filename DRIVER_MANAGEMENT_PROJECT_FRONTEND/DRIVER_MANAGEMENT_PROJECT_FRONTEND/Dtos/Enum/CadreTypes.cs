using System.ComponentModel.DataAnnotations;

namespace DRIVER_MANAGEMENT_PROJECT_FRONTEND.Dto.Enum
{
    public enum CadreTypes
    {
        [Display(Name ="URBAN PUBLIC TRANSPORT")]
        URBAN_PUBLIC_TRANSPORT = 0,       
        [Display(Name = "INTERCITY TRANSPORT")]
        INTERCITY_TRANSPORT = 1,         
        [Display(Name = "CORPORATE SHUTTLE SERVICE")]
        CORPORATE_SHUTTLE_SERVICE = 2,    
        [Display(Name = "TOURISM AND CHARTER SERVICE")]
        TOURISM_AND_CHARTER_SERVICE = 3,
        [Display(Name = "NIGHT SHIFT OPERATION")]
        NIGHT_SHIFT_OPERATION = 4,
        [Display(Name = "BACKUP AND RELIEF STAFF")]
        BACKUP_AND_RELIEF_STAFF = 5,
        [Display(Name = "TRAINING AND EVAULATION")]
        TRAINING_AND_EVALUATION = 6,
        [Display(Name = "VIP AND PRIVATE TRANSPORT")]
        VIP_AND_PRIVATE_TRANSPORT = 7,
        [Display(Name = "MUNICIPAL STAFF TRANSPORT")]
        MUNICIPAL_STAFF_TRANSPORT = 8,
        [Display(Name = "AIRPORT LOGISTIC TRANSFER ")]
        AIRPORT_LOGISTIC_TRANSFER = 9   
    }
}
