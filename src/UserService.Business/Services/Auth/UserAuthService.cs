using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using UserService.Business.Application.Events;
using UserService.DataAccess.Common;
using UserService.DataAccess.Common.Errors;
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
        private readonly IPublisher _publisher;
        public UserAuthService(
            IUserRepository userRepository,
            IJwtService jwtService,
            IPasswordHasher passwordHasher,
            IPublisher publisher)
        {
            _userRepository = userRepository;
            _jwtService = jwtService;
            _passwordHasher = passwordHasher;
            _publisher = publisher;
        }

        public async Task<Result<AuthResponse>> RegisterAsync(RegisterRequest request)
        {
            
            if (await _userRepository.ExistsAsync(request.Email))
                return Result<AuthResponse>.Failure(Errors.EmailAlreadyExists);
            

            var userExist = await _userRepository.FindByUserNameAsync(request.Username);
                if (userExist != null)
            {
                return Result<AuthResponse>.Failure(Errors.UsernameAlreadyExists);
                
            }
            var user = new User
            {
                Id=Guid.NewGuid(),
                UserName = request.Username,
                FirstName=request.FirstNama,
                LastName=request.LastNama,
                Email = request.Email,
                Role = request.Role,
                Phone=request.Phone,
                DateReg=DateTime.UtcNow
            };

            user.PasswordHash = _passwordHasher.Hash(request.Password);
            var token = _jwtService.GenerateAuthResponse(user);
            user.RefreshToken = token.Token;
            user.RefreshTokenExpiry = token.Expiration;
            await _userRepository.AddAsync(user);
            await _userRepository.SaveChangesAsync();
            await _publisher.Publish(new EntityEvent<Guid>(user, $"User {user.UserName} is registered"));
            return Result<AuthResponse> .Success(token);
        }

        public async Task<Result<AuthResponse>> LoginAsync(LoginRequest request)
        {
            var user = await _userRepository.FindByUserNameAsync(request.Username);
             if(user is null)
               return  Result<AuthResponse>.Failure(Errors.InvalidCredentials);

            if (!_passwordHasher.Verify( request.Password,user.PasswordHash))
                return Result<AuthResponse>.Failure(Errors.InvalidCredentials);

            await _publisher.Publish(new EntityEvent<Guid>(user, $"User {user.UserName},{user.Email} is login"));
            return Result<AuthResponse>.Success(_jwtService.GenerateAuthResponse(user));
        }

        public async Task LogoutAsync(string username)
        {
            var user = await _userRepository.FindByUserNameAsync(username);
            if (user is not null)
            {
                user.RefreshToken = "";
                user.RefreshTokenExpiry = null;
                await _userRepository.UpdateAsync(user);
                await _userRepository.SaveChangesAsync();
            }
            await _publisher.Publish(new EntityEvent<Guid>(user, $"User {user.UserName},{user.Email} is logout"));
        }
    }
}
