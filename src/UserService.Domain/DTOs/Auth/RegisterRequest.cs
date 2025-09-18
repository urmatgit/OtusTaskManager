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
    string FirstName,
    string LastName,
    string Username,
    string Email,
    string Phone,
    string Password,
    UserRole Role = UserRole.User);
}
