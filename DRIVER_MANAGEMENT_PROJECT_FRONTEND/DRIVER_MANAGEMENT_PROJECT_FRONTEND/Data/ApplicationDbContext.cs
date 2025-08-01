using DRIVER_MANAGEMENT_PROJECT_FRONTEND.Areas.Identity.Pages.Account;
using DRIVER_MANAGEMENT_PROJECT_FRONTEND.Dto;
using DRIVER_MANAGEMENT_PROJECT_FRONTEND.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DRIVER_MANAGEMENT_PROJECT_FRONTEND.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
