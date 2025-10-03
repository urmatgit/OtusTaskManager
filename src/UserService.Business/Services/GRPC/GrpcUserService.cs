using Grpc.Core;
using MapsterMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.OutputCaching;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.Business.Application.Projects.Commands.DeleteProject;
using UserService.Business.Application.Users;
using UserService.Business.Application.Users.Queries.GetAll;
using UserService.DataAccess.Entities;
using UserService.DataAccess.Enums;
using UserService.DataAccess.Persistence.Repositories.Auth;
namespace UserService.Business.Services.GRPC
{

    //public interface IGrpcUserService
    //{
    //    Task<UserResponse> GetUserAsync(string userId);
    //    Task<List<UserResponse>> GetUsersAsync(List<string> userIds);
    //}

    public class GrpcUserService : UserService.UserServiceBase
    {
        
        private readonly ILogger<GrpcUserService> _logger;
        private readonly IUserRepository _userRepository;
        public GrpcUserService(ILogger<GrpcUserService> logger, IUserRepository userRepository)
        {
            _logger = logger;
            _userRepository = userRepository;
        }

        public override async Task<UserResponseGRPC> GetUser(GetUserRequestGRPC request, ServerCallContext context)
        {
            var user=await _userRepository.GetAsync(Guid.Parse(request.UserId));
            if (user != null)
            {
                return new UserResponseGRPC()
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    UserId = user.Id.ToString(),
                    DisplayName = user.UserName
                };
            }
            return await base.GetUser(request, context);
        }
        public override async Task<UsersResponseGRPC> GetUsers(GetUsersRequestGRPC request, ServerCallContext context)
        {
            var users = await _userRepository.GetAllAsync(CancellationToken.None, asNoTracking: true);
            if (users != null && users.Count>0) {
                var userResponse = from user in users
                                   select new UserResponseGRPC()
                                   {
                                       FirstName = user.FirstName,
                                       LastName = user.LastName,
                                       Email = user.Email,
                                       UserId = user.Id.ToString(),
                                       DisplayName = user.UserName
                                   };
                var resulst = new UsersResponseGRPC();
                resulst.Users.AddRange(userResponse);
                return resulst;
                 
            }
            return await base.GetUsers(request, context);
        }

       
    }
}
