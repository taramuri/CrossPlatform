using System.Security.Claims;

namespace lab5_lab6.Services
{
    public class Auth0UserService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<Auth0UserService> _logger;

        public Auth0UserService(IConfiguration configuration, ILogger<Auth0UserService> logger)
        {
            _configuration = configuration;
            _logger = logger;
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