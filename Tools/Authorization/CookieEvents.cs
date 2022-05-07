using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

namespace FoodSharing.Tools.Authorization
{
    public static class CookieEvents
    {
        public static async Task Authenticate(string username, HttpContext httpContext)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, username)
            };

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Cookies");

            await httpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
        }

        public static async Task SignOut(HttpContext httpContext)
        {
            await httpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }
    }
}
