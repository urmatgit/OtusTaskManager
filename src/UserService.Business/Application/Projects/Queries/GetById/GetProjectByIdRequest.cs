using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.DataAccess.Common;

namespace UserService.Business.Application.Projects.Queries.GetById
{
    public  record GetProjectByIdRequest(Guid id): IRequest<Result<ProjectResponse>>;
}
