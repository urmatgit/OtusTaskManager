using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.DataAccess.Enums;

namespace UserService.DataAccess.DTOs.Auth
{
    public record RegisterRequest(
    [Required] string Username,
    [Required, EmailAddress] string Email,
    [Required, MinLength(6)] string Password,
    ProjectRole Role = ProjectRole.User);
}
