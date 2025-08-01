using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserService.DataAccess.DTOs.Auth;

namespace UserService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly ILogger<AccountController> _logger;
        private readonly HttpClient _httpClient;
        private readonly KeycloakUserService _keycloakService;
        public AccountController(IConfiguration config, ILogger<AccountController> logger, HttpClient httpClient,KeycloakUserService keycloakService    )
        {
            _config = config;
            _logger = logger;
            _httpClient = httpClient;
            _keycloakService = keycloakService;
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetProfile()
        {
            var profile = new
            {
                Username = User.Identity?.Name,
                FirstName = User.FindFirst("given_name")?.Value,
                LastName = User.FindFirst("family_name")?.Value,
                Email = User.FindFirst("email")?.Value,
                Phone = User.FindFirst("phoneNumber")?.Value, // из атрибута
                Roles = User.Claims
                    .Where(c => c.Type == "realm_access" || c.Type == "client_roles")
                    .Select(c => c.Value)
            };

            return Ok(profile);
        }

        [Authorize(Roles = "admin")]
        [HttpGet("admin")]
        public IActionResult AdminOnly() => Ok("Доступ только для админов");

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest registerRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var success = await _keycloakService.CreateUserAsync(registerRequest);

            if (success)
            {
                return Ok(new { message = "Регистрация успешна. Проверьте email." });
            }

            return StatusCode(500, new { message = "Ошибка при создании пользователя." });

        }

        //private async Task<string> GetAdminToken()
        //{
        //    throw new NotImplementedException();
        //}
    }
}
