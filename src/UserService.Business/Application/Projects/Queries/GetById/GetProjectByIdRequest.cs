using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserService.Business.Application.Projects.Queries.GetById
{
    public  record GetProjectByIdRequest(Guid id): IRequest<ProjectResponse>;
}
