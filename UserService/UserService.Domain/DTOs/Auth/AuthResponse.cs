using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserService.DataAccess.DTOs.Auth
{
    public record AuthResponse(
    string Token,
    DateTime Expiration,
    string Username,
    string Role);
}
