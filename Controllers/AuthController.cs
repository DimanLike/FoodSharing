using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Builder;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using FoodSharing.Models;
using Npgsql;
using System.Configuration;

namespace FoodSharing.Controllers
{

    record class Person(string Email, string Password);

    [AllowAnonymous]
    public class AuthController : Controller
    {
        private DataConnection _db;
        private readonly IConfiguration _config;

        public AuthController(IConfiguration config)
        {
            _config = config;
        }

        public string ConnectionString()
        {
            return _config.GetConnectionString("DefaultConnection");
        }

        List<Person> people = new List<Person>
        {
            new Person("tom@gmail.com", "12345"),
            new Person("bob@gmail.com", "55555")
        };

        [HttpGet]
        [Route("Login")]
        public IActionResult Login()
        {
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
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string? returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View("Login", model);
            }

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

            await Authenticate(model.Email);

            return RedirectToAction("Privacy", "Home");
        }


        [Authorize]
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Login", "Auth");
        }

        [HttpPost]
        [Route("RegisterUser")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RegisterUser(RegistrationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Registration", model);
            }
            Guid uuid1 = Guid.NewGuid();
 
            //Исправить
            //await _db.ExecuteNonQuery($@"INSERT INTO usertest (id, email, password) VALUES(N'{uuid1}', N'{model.Email}', N'{model.Password}', 'false')");
            //await using var cmd = new NpgsqlCommand("INSERT INTO usertest (id, email, password) VALUES (@id, @email, @password)", conn)
            await DBRegisterUser(model);

            await Authenticate(model.Email);

            return RedirectToAction("Privacy", "Home");
        }

        private async Task Authenticate(string username)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, username)
            };

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Cookies");
 
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
        }

        private async Task DBRegisterUser(RegistrationViewModel model)
        {
            var connectionString = ConnectionString();

            List<RegistrationViewModel> models = new List<RegistrationViewModel>() { model };

            await using var conn = new NpgsqlConnection(connectionString);
            await conn.OpenAsync();

            Guid uuid1 = Guid.NewGuid();

            await using var cmd = new NpgsqlCommand("INSERT INTO usertest (id, email, password) VALUES (@id, @email, @password)", conn)
            {
                Parameters =
                    {
                    new("id", uuid1),
                    new("email", model.Email),
                    new("password", model.Password)
                    }
            };

            await cmd.ExecuteNonQueryAsync();
        }
    }
}
