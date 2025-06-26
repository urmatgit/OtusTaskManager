
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using UserService.Business.Application.Events;
using UserService.Business.Services.Mail;
using UserService.DataAccess.Common;
using UserService.DataAccess.Common.Errors;
using UserService.DataAccess.DTOs.Auth;
using UserService.DataAccess.Entities;
using UserService.DataAccess.Entities.Events;
using UserService.DataAccess.Persistence.Repositories.Auth;

namespace UserService.Business.Services.Auth
{
    public class UserAuthService : IUserAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtService _jwtService;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IPublisher _publisher;
        private readonly IMailService _mailService;
        public UserAuthService(
            IUserRepository userRepository,
            IJwtService jwtService,
            IPasswordHasher passwordHasher,
            IPublisher publisher,
            IMailService mailService)
        {
            _userRepository = userRepository;
            _jwtService = jwtService;
            _passwordHasher = passwordHasher;
            _publisher = publisher;
            _mailService = mailService;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="origin">Full URL for confirm an email</param>
        /// <returns></returns>
        public async Task<Result<AuthResponse>> RegisterAsync(RegisterRequest request, string origin="")
        {
            
            if (await _userRepository.ExistsAsync(request.Email))
                return Result<AuthResponse>.Failure(Errors.EmailAlreadyExists);
            

            var userExist = await _userRepository.FindByUserNameAsync(request.Username);
                if (userExist != null)
            {
                return Result<AuthResponse>.Failure(Errors.UsernameAlreadyExists);
                
            }
            var user = new User(request.Username, request.FirstName, request.LastName, request.Email, request.Phone, request.Role, _passwordHasher.Hash(request.Password));
            
            var token = _jwtService.GenerateAuthResponse(user);
            //user.RefreshToken = token.Token;
            //user.RefreshTokenExpiry = token.Expiration;
            await _userRepository.AddAsync(user);
            await _userRepository.SaveChangesAsync();

            // send confirmation mail
            if (!string.IsNullOrEmpty(user.Email) && !string.IsNullOrEmpty(origin)) {
                string emailVerificationUri = await GetEmailVerificationUriAsync(user, origin);
                //TODO Send email
                var mailRequest = new MailRequest(
                    new System.Collections.ObjectModel.Collection<string> { user.Email }
                    , "Подтвердите регистрацию"
                    , emailVerificationUri
                    );

                //TODO jobservice send with handfire
                
                await _mailService.SendAsync(mailRequest, CancellationToken.None);
            }
            //await _publisher.Publish(new EntityEvent<Guid>(user, $"User {user.UserName} is registered"));
            return Result<AuthResponse> .Success(token);
        }
        private async Task<string> GetEmailVerificationUriAsync(User user,string origin)
        {
            string code = user.EmailConfirmCode ?? Guid.NewGuid().ToString();
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            const string route = "api/users/confirm-email/";
            var endpointUri = new Uri(string.Concat($"{origin}/", route));
            string verificationUri = QueryHelpers.AddQueryString(endpointUri.ToString(), GlobalConstantes.UserId, user.Id.ToString());
            verificationUri = QueryHelpers.AddQueryString(verificationUri, GlobalConstantes.Code, code);
            
            return verificationUri;
        }
        bool IsValidEmail(string email)
        {
            var trimmedEmail = email.Trim();

            if (trimmedEmail.EndsWith("."))
            {
                return false; // suggested by @TK-421
            }
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == trimmedEmail;
            }
            catch
            {
                return false;
            }
        }
        public async Task<Result<AuthResponse>> LoginAsync(LoginRequest request)
        {
            User user = null; 
            //Авторизация с почтой или логином
            if (IsValidEmail(request.Username))
            {
                user = await _userRepository.FindByUserEmailAsync(request.Username);
            }
            else 
                user = await _userRepository.FindByUserNameAsync(request.Username);
             if(user is null)
               return  Result<AuthResponse>.Failure(Errors.InvalidCredentials);

            if (!_passwordHasher.Verify( request.Password,user.PasswordHash))
                return Result<AuthResponse>.Failure(Errors.InvalidCredentials);
            //user.AddDomainEvent(new UserLoginEvent(user));
            await _publisher.Publish(new UserLoginEvent(user));
            return Result<AuthResponse>.Success(_jwtService.GenerateAuthResponse(user));
        }

        public async Task LogoutAsync(string username)
        {
            var user = await _userRepository.FindByUserNameAsync(username);
            if (user is not null)
            {
                //user.RefreshToken = "";
                //user.RefreshTokenExpiry = null;
                _userRepository.Update(user);
                //user.AddDomainEvent(new UserLogoutEvent(user));
                await _userRepository.SaveChangesAsync();
                
            }
            await _publisher.Publish(new UserLogoutEvent(user));
            // await _publisher.Publish(new EntityEvent<Guid>(user, $"User {user.UserName},{user.Email} is logout"));
        }

        public async Task<string> ConfirmEmailAsync(Guid userId, string code)
        {
            

            var user = await _userRepository.GetUsersSet()
                .Where(u => u.Id == userId && !u.EmailConfirmed)
                .FirstOrDefaultAsync();

            _ = user ?? throw new Exception("An error occurred while confirming E-Mail.");

            code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
            var result = await _userRepository.ConfirmEmailAsync(user, code);

            return result
                ? string.Format("Account Confirmed for E-Mail {0}. You can now use the /api/tokens endpoint to generate JWT.", user.Email)
                : throw new Exception(string.Format("An error occurred while confirming {0}", user.Email));
        }

       
    }
}
