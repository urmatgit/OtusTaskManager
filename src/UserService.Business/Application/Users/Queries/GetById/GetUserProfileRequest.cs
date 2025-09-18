using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.DataAccess.Common;
using UserService.DataAccess.Entities;

namespace UserService.Business.Application.Users.Queries.GetById
{
    public record GetUserProfileRequest(Guid? userId): IRequest<Result<UserResponse>>;
    
}
