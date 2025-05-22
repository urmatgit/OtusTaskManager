using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.DataAccess.Common;

namespace UserService.Business.Application.Projects.Commands.DeleteProject
{
    public record DeleteProjectRequest(Guid id):IRequest<Result<bool>>;
    
}
