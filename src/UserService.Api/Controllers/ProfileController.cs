using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class ProfileController : ControllerBase
{
    [Authorize]
    [HttpGet]
    public IActionResult Get()
    {
        return Ok(new
        {
            Username = User.Identity?.Name,
            Email = User.FindFirst("email")?.Value,
            Roles = User.FindAll("realm_access").Select(c => c.Value)
        });
    }

    [Authorize(Roles = "user")]
    [HttpGet("user")]
    public IActionResult UserOnly() => Ok("Доступ только для пользователей");
}