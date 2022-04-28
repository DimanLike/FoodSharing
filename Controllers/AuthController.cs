using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace FoodSharing.Controllers
{

    record class Person(string Email, string Password);

    [AllowAnonymous]
    public class AuthController : Controller
    {
        List<Person> people = new List<Person>
        {
            new Person("tom@gmail.com", "12345"),
            new Person("bob@gmail.com", "55555")
        };

        [HttpGet]
        [Route("Login")]
        public IActionResult Login()
        {
            //HttpContext.Response.ContentType = "text/html; charset=utf-8";

            return View();
        }

        [HttpGet]
        [Route("Registration")]
        public IActionResult Registration()
		{
            return View();
		}

        [HttpPost]
        [Route("Login")]
        public ActionResult Login(string? returnUrl)
        {
            // получаем из формы email и пароль
            var form = HttpContext.Request.Form;
            // если email и/или пароль не установлены, посылаем статусный код ошибки 400
            if (!form.ContainsKey("email") || !form.ContainsKey("password"))
                return View();

            string email = form["email"];
            string password = form["password"];

            // находим пользователя 
            Person? person = people.FirstOrDefault(p => p.Email == email && p.Password == password);
            // если пользователь не найден, отправляем статусный код 401
            if (person is null)
                return View();

            var claims = new List<Claim> { new Claim(ClaimTypes.Name, person.Email) };
            // создаем объект ClaimsIdentity
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Cookies");
            // установка аутентификационных куки
            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

            return RedirectToAction("Privacy", "Home");
        }

        [HttpGet]
        public async Task<IResult> LogOut(HttpContext context)
        {
            await context.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return Results.Redirect("/Login");
        }
    }
}
