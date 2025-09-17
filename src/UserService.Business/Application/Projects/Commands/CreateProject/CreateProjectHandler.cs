using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.Business.Application.Events;

using UserService.Business.Services.Auth;
using UserService.DataAccess.Common;
using UserService.DataAccess.Entities;
using UserService.DataAccess.Persistence.Repositories;

namespace UserService.Business.Application.Projects.Commands.CreateProject
{
    public class CreateProjectHandler : IRequestHandler<CreateProjectRequest, Result<ProjectResponse>>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly ICurrentUser _curentUser;
        private readonly IMapper _mapper;
        private readonly IPublisher _publisher;
        public CreateProjectHandler(IProjectRepository projectRepsitory,ICurrentUser curentUser,IMapper mapper,IPublisher publisher)
        {
            _projectRepository = projectRepsitory;
            _curentUser = curentUser;
            _mapper = mapper;
            _publisher = publisher;
        }
        public async Task<Result<ProjectResponse>> Handle(CreateProjectRequest request, CancellationToken cancellationToken)
        {
            var existProject = _projectRepository.FindByName(request.name);
            if (existProject != null)
                return Result<ProjectResponse>.Failure("Проект с таким названием уже существует!");
            var project = new Project(request.name, _curentUser.GetUserId());
            
            await _projectRepository.AddAsync(project);
            await _projectRepository.SaveChangesAsync();
            
            await _publisher.Publish(new EntityEvent<Guid>(project, $"Create project"));
            return  Result<ProjectResponse>.Success(_mapper.Map<ProjectResponse>(project));  // new ProjectResponse(project.Id,project.Name,project.Created,project.UserId);
        } 
    }
}
