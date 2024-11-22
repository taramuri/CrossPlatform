using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text;
using System.Security.Claims;
using lab5_lab6.Services;
using Auth0.AspNetCore.Authentication;

namespace lab5_lab6.Controllers
{
    public class AccountController : Controller
    {
        private readonly Auth0UserService _auth0UserService;
        private readonly IConfiguration _configuration;

        public AccountController(Auth0UserService auth0UserService, IConfiguration configuration)
        {
            _auth0UserService = auth0UserService;
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult Login(string returnUrl = "/")
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model, string? returnUrl = "/")
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var httpClient = new HttpClient();
            var clientId = _configuration["Auth0:ClientId"];
            var clientSecret = _configuration["Auth0:ClientSecret"];
            var domain = _configuration["Auth0:Domain"];

            var content = new StringContent(JsonSerializer.Serialize(new
            {
                grant_type = "password",
                username = model.Email,
                password = model.Password,
                audience = _configuration["Auth0:Audience"], 
                client_id = clientId,
                client_secret = clientSecret
            }), Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync($"https://{domain}/oauth/token", content);

            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError(string.Empty, "Невірний email або пароль.");
                return View(model);
            }

            var responseJson = await response.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<Dictionary<string, object>>(responseJson);

            if (result == null || !result.ContainsKey("access_token"))
            {
                ModelState.AddModelError(string.Empty, "Помилка авторизації. Спробуйте ще раз.");
                return View(model);
            }

            var token = result["access_token"].ToString();

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, model.Email),
                new Claim("AccessToken", token)
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties
            {
                IsPersistent = model.RememberMe
            };

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity), authProperties);

            return LocalRedirect(returnUrl ?? "/");
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            var authenticationProperties = new LogoutAuthenticationPropertiesBuilder()
                .WithRedirectUri(Url.Action("Index", "Home"))
                .Build();

            await HttpContext.SignOutAsync(Auth0Constants.AuthenticationScheme, authenticationProperties);
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        public IActionResult Profile()
        {
            var userProfile = _auth0UserService.GetUserProfile(User);
            if (userProfile == null)
            {
                return RedirectToAction("Login");
            }
            return View(userProfile);
        }

        [Authorize]
        public IActionResult Claims()
        {
            return View();
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var httpClient = new HttpClient();
            var domain = _configuration["Auth0:Domain"];
            var clientId = _configuration["Auth0:ClientId"];
            var clientSecret = _configuration["Auth0:ClientSecret"];

            var content = new StringContent(JsonSerializer.Serialize(new
            {
                email = model.Email,
                password = model.Password,
                connection = _configuration["Auth0:Connection"], 
                client_id = clientId,
                client_secret = clientSecret
            }), Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync($"https://{domain}/dbconnections/signup", content);

            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError(string.Empty, "Не вдалося створити обліковий запис. Перевірте введені дані.");
                return View(model);
            }

            TempData["RegistrationSuccess"] = "Ваш обліковий запис успішно створено. Тепер ви можете увійти.";
            return RedirectToAction("Login");
        }
    }
}
