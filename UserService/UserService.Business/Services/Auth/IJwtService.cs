using UserService.DataAccess.DTOs.Auth;
using UserService.DataAccess.Entities;

namespace UserService.Business.Services.Auth

{
    public interface IJwtService
    {
        public AuthResponse GenerateAuthResponse(User user);
    }
}
