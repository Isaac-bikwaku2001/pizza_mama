using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace pizza_mama.Pages.Admin
{
    public class LoginModel : PageModel
    {
        private IConfiguration configuration;
        public bool DisplayInvalidAccountMessage = false;

        public LoginModel(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public IActionResult OnGet()
        {
            if (this.HttpContext.User.Identity.IsAuthenticated)
            {
                return Redirect("/Admin/Pizzas");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string username, string password, string ReturnUrl)
        {
            var authSection = configuration.GetSection("Auth");

            string adminLogin = authSection["AdminLogin"];
            string adminPassword = authSection["AdminPassword"];

            if((username == adminLogin) && (password == adminPassword))
            {
                this.DisplayInvalidAccountMessage = false;

                var claims = new List<Claim>
                {
                new Claim(ClaimTypes.Name, username)
                };
                var claimsIdentity = new ClaimsIdentity(claims, "Login");
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new
                ClaimsPrincipal(claimsIdentity));
                return Redirect(ReturnUrl == null ? "/Admin/Pizzas" : ReturnUrl);
            }

            this.DisplayInvalidAccountMessage = true;
            return Page();
        }

        public async Task<IActionResult> OnGetLogout()
        {
            await HttpContext.SignOutAsync();
            return Redirect("/Admin/Login");
        }
    }
}
