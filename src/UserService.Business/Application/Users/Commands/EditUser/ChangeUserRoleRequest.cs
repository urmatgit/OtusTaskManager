using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.DataAccess.Common;
using UserService.DataAccess.Entities;
using UserService.DataAccess.Enums;

namespace UserService.Business.Application.Users.Commands.EditUser
{
    public record ChangeUserRoleRequest(Guid userid,ProjectRole newRole):IRequest<Result<UserResponse>>;
    
}
