using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.Business.Application.Projects;
using UserService.Business.Application.Projects.Commands.CreateProject;
using UserService.DataAccess.Common;

namespace UserService.Business.Application.ProjectsUsers.Commands.AddUserToProject
{
    
    public record RemoveUserFromProjectRequest(Guid id,  Guid userid) : IRequest<Result<ProjectWithUsersResponse>>;
}
