using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using lab13.Server.Models;
using LabLibrary;

namespace lab13.Server.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [HttpGet("GetInfo")]
        public IActionResult GetInfo()
        {
            return Ok(new { message = "Welcome to the Lab5 API!" });
        }

        [HttpGet("PrivacyInfo")]
        public IActionResult PrivacyInfo()
        {
            return Ok(new { message = "This is the privacy policy information." });
        }

        [HttpGet("Error")]
        public IActionResult Error()
        {
            var requestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            _logger.LogError($"Error occurred. RequestId: {requestId}");
            return Problem(detail: "An unexpected error occurred.", instance: requestId);
        }
    }
}
