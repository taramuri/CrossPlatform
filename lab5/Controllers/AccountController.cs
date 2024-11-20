using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Auth0.AspNetCore.Authentication;
using lab5.Services;

namespace lab5.Controllers
{
    public class AccountController : Controller
    {
        private readonly Auth0UserService _auth0UserService;

        public AccountController(Auth0UserService auth0UserService)
        {
            _auth0UserService = auth0UserService;
        }

        [HttpGet]
        public async Task Login(string returnUrl = "/")
        {
            var authenticationProperties = new LoginAuthenticationPropertiesBuilder()
                .WithRedirectUri(returnUrl)
                .Build();

            await HttpContext.ChallengeAsync(Auth0Constants.AuthenticationScheme, authenticationProperties);
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
    }
}