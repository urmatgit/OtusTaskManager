using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using UserService.DataAccess.Common;
using UserService.DataAccess.DTOs.Auth;
using UserService.DataAccess.Entities;
using UserService.DataAccess.Persistence.Repositories.Auth;

namespace UserService.Business.Services.Auth
{
    public class UserAuthService : IUserAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtService _jwtService;
        private readonly IPasswordHasher _passwordHasher;

        public UserAuthService(
            IUserRepository userRepository,
            IJwtService jwtService,
            IPasswordHasher passwordHasher)
        {
            _userRepository = userRepository;
            _jwtService = jwtService;
            _passwordHasher = passwordHasher;
        }

        public async Task<Result<AuthResponse>> RegisterAsync(RegisterRequest request)
        {
            if (await _userRepository.ExistsAsync(request.Email))
                return Result<AuthResponse>.Failure("Email already exists");
            

            var userExist = await _userRepository.FindByUserNameAsync(request.Username);
                if (userExist != null)
            {
                return Result<AuthResponse>.Failure("Username already exists");
                
            }
            var user = new User
            {
                UserName = request.Username,
                Email = request.Email,
                Role = request.Role,
                Phone=request.Phone
            };

            user.PasswordHash = _passwordHasher.Hash(request.Password);
            await _userRepository.AddAsync(user);

            return Result<AuthResponse> .Success( _jwtService.GenerateAuthResponse(user));
        }

        public async Task<Result<AuthResponse>> LoginAsync(LoginRequest request)
        {
            var user = await _userRepository.FindByUserNameAsync(request.Username);
             if(user is null)
               return  Result<AuthResponse>.Failure("Invalid credentials");

            if (!_passwordHasher.Verify( request.Password,user.PasswordHash))
                return Result<AuthResponse>.Failure("Invalid credentials");
            

            return Result<AuthResponse>.Success(_jwtService.GenerateAuthResponse(user));
        }
    }
}
