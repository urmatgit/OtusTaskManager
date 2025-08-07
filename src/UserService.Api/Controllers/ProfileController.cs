using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
            //Roles = User.FindAll("realm_access").Select(c => c.Value)
            Roles=User.FindAll(ClaimTypes.Role).Select(c=>c.Value)
        });
    }

    // 🔐 Только для пользователей с ролью "user"
    [Authorize(Roles = "User")]
    [HttpGet("user-data")]
    public IActionResult GetUserData()
    {
        return Ok(new
        {
            Message = "Привет, пользователь!",
            Data = "Данные для обычных пользователей"
        });
    }

    // 🔐 Только для пользователей с ролью "admin"
    [Authorize(Roles = "Admin")]
    [HttpGet("admin-data")]
    public IActionResult GetAdminData()
    {
        return Ok(new
        {
            Message = "Привет, администратор!",
            Data = "Конфиденциальные данные для админов"
        });
    }

    // ✅ Проверка роли в коде (программная проверка)
    [Authorize]
    [HttpGet("check-role")]
    public IActionResult CheckRole()
    {
        bool isAdmin = User.IsInRole("Admin");
        bool isUser = User.IsInRole("User");

        return Ok(new
        {
            IsAdmin = isAdmin,
            IsUser = isUser,
            RoleBasedAccess = isAdmin ? "full" : isUser ? "limited" : "none"
        });
    }
}