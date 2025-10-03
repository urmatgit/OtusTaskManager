using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.Business.Application.Users;

namespace UserService.Business.Services.GRPC
{
    public interface IGrpcUserService
    {
        Task<UserResponseGRPC> GetUserAsync(string userId);
        Task<List<UserResponseGRPC>> GetUsersAsync(List<string> userIds);
    }
}
