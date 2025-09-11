using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.Business.Application.Users;
using UserService.DataAccess.Common;

namespace UserService.Business.Application.Projects.Queries.GetById
{
    public  record GetUsersByProjectRequest(Guid id): IRequest<Result<List<UserResponse>>>;
}
