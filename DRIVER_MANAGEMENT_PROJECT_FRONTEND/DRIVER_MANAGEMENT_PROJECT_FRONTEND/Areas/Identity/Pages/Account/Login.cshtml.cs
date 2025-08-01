// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using DRIVER_MANAGEMENT_PROJECT_FRONTEND.Data;
using DRIVER_MANAGEMENT_PROJECT_FRONTEND.Dto;
using DRIVER_MANAGEMENT_PROJECT_FRONTEND.Users;
using DRIVER_MANAGEMENT_PROJECT_FRONTEND.Utils;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
namespace DRIVER_MANAGEMENT_PROJECT_FRONTEND.Areas.Identity.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationDbContext _context;
        private readonly string _baseUrl;


        public LoginModel(
         UserManager<ApplicationUser> userManager,
         SignInManager<ApplicationUser> signInManager,
         ApplicationDbContext context,
         IOptions<ApiSettings> options)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
            _baseUrl = options.Value.BaseUrl;
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public string ReturnUrl { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [TempData]
        public string ErrorMessage { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public class InputModel
        {
            [Required(ErrorMessage = "Phone number is required.")]
            [Display(Name = "Phone Number")]
            public string PhoneNumber { get; set; }

            [Required(ErrorMessage = "Registration number is required.")]
            [Display(Name = "Registration Number")]
            public string RegistrationNumber { get; set; }

            [Display(Name = "Remember Me")]
            public bool RememberMe { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            returnUrl ??= Url.Content("~/");

            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            ReturnUrl = returnUrl;
        }

        private async Task<List<RoleDto>> GetRolesFromSpringBackendAsync(string registrationNumber, string phone)
        {
            using (var httpClient = new HttpClient())
            {
                string regNo = registrationNumber.Trim().ToUpper();
                string phoneNo = phone.Trim();
                var springLoginUrl = $"{_baseUrl}/role/login?regNo={regNo}&phone={phoneNo}";

                var response = await httpClient.PostAsync(springLoginUrl, null);

                if (!response.IsSuccessStatusCode)
                    return null;

                var jsonString = await response.Content.ReadAsStringAsync();

                var roles = System.Text.Json.JsonSerializer.Deserialize<List<RoleDto>>(jsonString,
                    new System.Text.Json.JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

                return roles;
            }
        }
        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");

            if (!ModelState.IsValid)
                return Page();

            var user = await _userManager.Users
                .FirstOrDefaultAsync(u => u.RegistrationNumber == Input.RegistrationNumber);

            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Invalid Registration Number.");
                return Page();
            }

            var springRoles = await GetRolesFromSpringBackendAsync(Input.RegistrationNumber, Input.PhoneNumber);

            if (springRoles == null || !springRoles.Any())
            {
                ModelState.AddModelError(string.Empty, "Phone or registration number is wrong!.");
                return Page();
            }

            var springRoleNames = springRoles
                .Select(r => ((RolesEnum)r.RoleName).GetDisplayName())
                .OrderBy(x => x)
                .ToList();

            var localRoleNames = await _userManager.GetRolesAsync(user);
            localRoleNames = localRoleNames.OrderBy(x => x).ToList();

            bool rolesMatch = springRoleNames.SequenceEqual(localRoleNames);

            if (!rolesMatch)
            {
                ModelState.AddModelError(string.Empty, "Role mismatch.");
                return Page();
            }

            var claims = new List<Claim>
                {   
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(ClaimTypes.Name, user.UserName ?? user.RegistrationNumber),
                    new Claim("RegistrationNumber", user.RegistrationNumber)
                };

            foreach (var role in localRoleNames)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(IdentityConstants.ApplicationScheme, principal, new AuthenticationProperties
            {
                IsPersistent = Input.RememberMe
            });

            if (localRoleNames.Contains("DRIVER"))
            {
                return RedirectToAction("DriverInfoPage", "DriverInfo", new { registrationNumber = user.RegistrationNumber });
            }
            else if (localRoleNames.Contains("CHIEF"))
            {
                return RedirectToAction("Index", "Driver");
            }
            else if (localRoleNames.Contains("ADMIN"))
            {
                return RedirectToAction("DriverIndex", "Admin");
            }

            return LocalRedirect(returnUrl); 
        }

    }
    public class Role
    {
        public int Id { get; set; }
        public int RoleName { get; set; } 
        public int PersonId { get; set; }
    }
    public enum RolesEnum
    {
        ADMIN = 0,
        CHIEF = 1,
        DRIVER = 2
    }
}
