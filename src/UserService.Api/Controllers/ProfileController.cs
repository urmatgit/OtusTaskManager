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
            Username = User.FindFirst("name")?.Value,
            Email = User.FindFirst("preferred_username")?.Value,
            Roles = User.FindAll("realm_access").Select(c => c.Value)
        });
    }

    [Authorize(Roles = "Admin")]
    [HttpGet("admin")]
    public IActionResult AdminOnly() => Ok("Доступ только для админам");
}