using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.Business.Application.Projects.Commands.CreateProject;
using UserService.DataAccess.Common;

namespace UserService.Business.Application.Projects.Commands.UpdateProject
{
    public record UpdateProjectRequest(Guid id, string name,Guid userid): CreateProjectRequest(name), IRequest<Result<ProjectResponse>>;
    
        //public string Name { get; set; }
        //public DateTime Created { get; set; }
        //public bool IsDeleted { get; set; }
        //public Guid UserId { get; set; }
    
}
