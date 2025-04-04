using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public CreateProjectHandler(IProjectRepository projectRepsitory,ICurrentUser curentUser,IMapper mapper)
        {
            _projectRepository = projectRepsitory;
            _curentUser = curentUser;
            _mapper = mapper;
        }
        public async Task<Result<ProjectResponse>> Handle(CreateProjectRequest request, CancellationToken cancellationToken)
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

            return  Result<ProjectResponse>.Success(_mapper.Map<ProjectResponse>(project));  // new ProjectResponse(project.Id,project.Name,project.Created,project.UserId);
        } 
    }
}
