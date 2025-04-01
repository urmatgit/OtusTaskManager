using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserService.DataAccess.DTOs.Auth
{
    public record LoginRequest(
    string Username,
     string Password);
}
