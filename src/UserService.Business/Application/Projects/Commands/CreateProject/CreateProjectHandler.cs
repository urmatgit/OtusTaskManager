using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.Business.Services.Auth;
using UserService.DataAccess.Entities;
using UserService.DataAccess.Persistence.Repositories;

namespace UserService.Business.Application.Projects.Commands.CreateProject
{
    public class CreateProjectHandler : IRequestHandler<CreateProjectCommand, ProjectResponse>
    {
        private readonly IProjectRepsitory _projectRepository;
        private readonly ICurrentUser _curentUser;
        public CreateProjectHandler(IProjectRepsitory projectRepsitory,ICurrentUser curentUser)
        {
            _projectRepository = projectRepsitory;
            _curentUser = curentUser;
        }
        public async Task<ProjectResponse> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
        {
            var project=new Project()
            {
                Id=Guid.NewGuid(),
                Name=request.name,
                UserId=_curentUser.GetUserId(),
                Created=DateTime.UtcNow,
            };
            await _projectRepository.AddAsync(project);
            await _projectRepository.SaveChangesAsync();

            return  new ProjectResponse(project.Id,project.Name,project.Created,project.UserId);
        }
    }
}
