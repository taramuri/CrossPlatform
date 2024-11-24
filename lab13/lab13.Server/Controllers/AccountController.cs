using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using lab13.Server.Models;
using lab13.Server.Services;
using System.Text.Json;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace lab13.Server.Controllers;


[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly Auth0UserService _auth0UserService;
    private readonly IConfiguration _configuration;

    public AccountController(Auth0UserService auth0UserService, IConfiguration configuration)
    {
        _auth0UserService = auth0UserService;
        _configuration = configuration;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
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
                        
            return Ok(new { Message = "User registered successfully." });
        }
        catch (Exception ex)
        {
            return BadRequest(new { Error = $"Error creating user: {ex.Message}" });
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new { Error = "Invalid model state", Details = ModelState });
        }

        try
        {
            UserProfileViewModel userProfile = await _auth0UserService.GetUser(model);
            if (userProfile == null)
            {
                return Unauthorized(new { Error = "Invalid credentials" });
            }

            List<Claim> claims = new()
            {
                new Claim(ClaimTypes.NameIdentifier, userProfile.Email),
                new Claim(ClaimTypes.Name, userProfile.FullName),
                new Claim(ClaimTypes.Email, userProfile.Email),
                new Claim(ClaimTypes.MobilePhone, userProfile.Phone)
            };

            ClaimsIdentity claimsIdentity = new(claims, "AuthScheme");
            ClaimsPrincipal claimsPrincipal = new(claimsIdentity);

            await HttpContext.SignInAsync("AuthScheme", claimsPrincipal);

            return Ok(new { Message = "Login successful.", UserProfile = userProfile });
        }
        catch (Exception ex)
        {
            return BadRequest(new { Error = $"Error authenticating user: {ex.Message}" });
        }
    }

    [HttpGet("profile")]
    [Authorize]
    public IActionResult Profile()
    {
        string alternativeValue = "N/A";
        ClaimsPrincipal user = HttpContext.User;

        UserProfileViewModel profileViewModel = new()
        {
            Email = user.FindFirst(ClaimTypes.Email)?.Value ?? alternativeValue,
            FullName = user.FindFirst(ClaimTypes.Name)?.Value ?? alternativeValue,
            Phone = user.FindFirst(ClaimTypes.MobilePhone)?.Value ?? alternativeValue
        };

        return Ok(profileViewModel);
    }

    [HttpPost("logout")]
    [Authorize]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync();
        return Ok(new { Message = "Logout successful." });
    }
}