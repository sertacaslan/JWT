using JWTDeneme.JWT;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JWTDeneme.Controllers
{
    [Authorize]//yetkilendirme
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };
        private readonly IJWTAuthenticationManager _authenticationManager;
        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger,IJWTAuthenticationManager authenticationManager)
        {
            _logger = logger;
            _authenticationManager = authenticationManager;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register([FromQuery]string username, [FromQuery] string password)
        {
            var token = _authenticationManager.Authenticate(username,password);
            if (token==null)
            {
                return Unauthorized();
            }

            return Ok();
        }
    }
}