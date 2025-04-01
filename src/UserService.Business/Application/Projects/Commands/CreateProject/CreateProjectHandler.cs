using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.DataAccess.Entities;
using UserService.DataAccess.Persistence.Repositories;

namespace UserService.Business.Application.Projects.Commands.CreateProject
{
    public class CreateProjectHandler : IRequestHandler<CreateProjectCommand, ProjectResponse>
    {
        private readonly IProjectRepsitory _projectRepository;
        public CreateProjectHandler(IProjectRepsitory projectRepsitory)
        {
            _projectRepository = projectRepsitory;
        }
        public async Task<ProjectResponse> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
        {
            var project=new Project()
            {
                Id=Guid.NewGuid(),
                Name=request.name,
                UserId=request.UserId,
                Created=DateTime.Now,
            };
            await _projectRepository.AddAsync(project);
            await _projectRepository.SaveChangesAsync();

            return  new ProjectResponse(project.Id,project.Name,project.Created,project.UserId);
        }
    }
}
