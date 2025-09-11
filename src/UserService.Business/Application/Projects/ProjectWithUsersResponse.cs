using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.Business.Application.Users;
using UserService.Business.Common.Interfaces;

namespace UserService.Business.Application.Projects
{
    public record ProjectWithUsersResponse(Guid id,string name,DateTime create,Guid owner, List<UserResponse> Users) : IDto;
    
}
