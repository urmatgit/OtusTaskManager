using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserService.Business.Services.Auth;
using UserService.DataAccess.Common.Errors;
using UserService.DataAccess.DTOs.Auth;

namespace UserService.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [AllowAnonymous]
    public class AuthController : ControllerBase
    {
        private readonly IUserAuthService _userService;
        private readonly IValidator<RegisterRequest> _registerValidator;
        private readonly IValidator<LoginRequest> _loginValidator;
        private readonly ILogger<AuthController> _logger;
        public AuthController(IUserAuthService userService,
            IValidator<RegisterRequest> registerValidator,
        IValidator<LoginRequest> loginValidator
            ,ILogger<AuthController> logger)
        {
            _userService = userService;
            _registerValidator = registerValidator;
            _loginValidator = loginValidator;
            _logger = logger;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            var validationResult = await _registerValidator.ValidateAsync(request);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.ToDictionary());
            var response = await _userService.RegisterAsync(request);
            if (response.IsFailure) {
                return BadRequest(response.Error);
            }
            _logger.LogInformation($"Register new user {response.Value.Username}");
            return Ok(response.Value);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var validationResult = await _loginValidator.ValidateAsync(request);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.ToDictionary());
            var response = await _userService.LoginAsync(request);
            if (response.IsFailure)
            {
                return BadRequest(response.Error);
            }
            return Ok(response.Value);
        }
        /// <summary>
        /// очищает поля   user.RefreshToken = ""; user.RefreshTokenExpiry = null;
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        [HttpPost("logout")]
        public async Task<IActionResult> Logout(string username)
        {
            
            if (string.IsNullOrEmpty(username))
                return BadRequest(Errors.UsernameIsRequired);
            await _userService.LogoutAsync(username);
            
            return Ok();
        }

    }
}
