using DRIVER_MANAGEMENT_PROJECT_FRONTEND.Dto;
using Microsoft.AspNetCore.Identity;
using System.Text.Json.Serialization;

namespace DRIVER_MANAGEMENT_PROJECT_FRONTEND.Users
{
    public class ApplicationUser :  IdentityUser
    {
        public int PersonId { get; set; }
        public string RegistrationNumber { get; set; }
    }
}
