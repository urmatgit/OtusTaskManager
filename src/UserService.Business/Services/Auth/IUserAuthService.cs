using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.DataAccess.Common;
using UserService.DataAccess.DTOs.Auth;

namespace UserService.Business.Services.Auth
{
    public interface IUserAuthService
    {
        Task<Result<AuthResponse>> RegisterAsync(RegisterRequest request, string origin="");
        Task<Result<AuthResponse>> LoginAsync(LoginRequest request);
        Task<string> ConfirmEmailAsync(Guid userId, string code);
        Task LogoutAsync(string username);
    }
}
