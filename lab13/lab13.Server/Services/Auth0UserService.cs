using Microsoft.AspNetCore.Authentication.BearerToken;
using System.Security.Claims;
using Auth0.ManagementApi;
using Auth0.ManagementApi.Models;
using Auth0.AuthenticationApi;
using Auth0.AuthenticationApi.Models;
using lab13.Server.Models;

namespace lab13.Server.Services
{
    public class Auth0UserService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<Auth0UserService> _logger;
        private readonly string _domain;
        private readonly string _audience;
        private readonly string _clientId;
        private readonly string _clientSecret;

        public Auth0UserService(IConfiguration configuration, ILogger<Auth0UserService> logger)
        {
            _configuration = configuration;
            _logger = logger;
            _domain = _configuration["Auth0:Domain"];
            _audience = _configuration["Auth0:Audience"];
            _clientId = _configuration["Auth0:ClientId"];
            _clientSecret = _configuration["Auth0:ClientSecret"];
        }

        public Auth0User GetUserProfile(ClaimsPrincipal user)
        {
            if (!user.Identity.IsAuthenticated)
            {
                _logger.LogWarning("Attempted to get profile for unauthenticated user");
                return null;
            }

            try
            {
                var auth0User = new Auth0User
                {
                    Username = user.FindFirst("nickname")?.Value
                        ?? user.FindFirst(ClaimTypes.Name)?.Value,
                    Email = user.FindFirst(ClaimTypes.Email)?.Value
                        ?? user.FindFirst("email")?.Value,
                    FullName = user.FindFirst(ClaimTypes.GivenName)?.Value
                        ?? user.FindFirst("name")?.Value,
                    Picture = user.FindFirst("picture")?.Value,
                    Phone = user.FindFirst(ClaimTypes.MobilePhone)?.Value
                        ?? user.FindFirst("phone_number")?.Value
                };

                // Validate required fields
                if (string.IsNullOrEmpty(auth0User.Email))
                {
                    _logger.LogWarning("User profile missing email");
                }

                return auth0User;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving user profile");
                return null;
            }
        }

        public async Task<UserProfileViewModel> GetUser(LoginViewModel model)
        {
            string alternativeValue = "N/A";
            AuthenticationApiClient authClient = new(new Uri($"https://{_domain}"));
            Auth0.AuthenticationApi.Models.AccessTokenResponse authResponse = await authClient.GetTokenAsync(new ResourceOwnerTokenRequest
            {
                Audience = _audience,
                ClientId = _clientId,
                ClientSecret = _clientSecret,
                Realm = "Username-Password-Authentication",
                Username = model.Email,
                Password = model.Password,
                Scope = "openid profile email"
            });

            ManagementApiClient managementClient = new(authResponse.AccessToken, new Uri($"https://{_domain}/api/v2"));

            UserInfo userInfo = await authClient.GetUserInfoAsync(authResponse.AccessToken);
            User user = await managementClient.Users.GetAsync(userInfo.UserId);


            return new UserProfileViewModel
            {
                FullName = user.UserMetadata?["FullName"]?.ToString() ?? alternativeValue,
                Phone = user.UserMetadata?["Phone"]?.ToString() ?? alternativeValue,
            };
        }
    }

    public class Auth0User
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string Picture { get; set; }
        public string Phone { get; set; }

        public bool HasRequiredFields =>
            !string.IsNullOrEmpty(Email) &&
            !string.IsNullOrEmpty(Username);
    }
}