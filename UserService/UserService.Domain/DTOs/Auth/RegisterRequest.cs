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
    string FirstNama,
    string LastNama,
    string Username,
    string Email,
    string Phone,
    string Password,
    ProjectRole Role = ProjectRole.User);
}
