using MapsterMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.Business.Application.Projects;
using UserService.Business.Application.Projects.Commands.UpdateProject;
using UserService.DataAccess.Common;
using UserService.DataAccess.Persistence.Repositories;

namespace UserService.Business.Application.ProjectsUsers.Commands.AddUserToProject
{
    public class AddUserToProjectRequestHandler : IRequestHandler<AddUserToProjectRequest, Result<ProjectWithUsersResponse>>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IMapper _mapper;
        public AddUserToProjectRequestHandler(IProjectRepository projectRepository, IMapper mapper)
        {
            _projectRepository = projectRepository;
            _mapper = mapper;
        }
        public async Task<Result<ProjectWithUsersResponse>> Handle(AddUserToProjectRequest request, CancellationToken cancellationToken)
        {
            var project = await _projectRepository.GetByIdWithUsersAsync(request.id);
            if (project == null)
            {
                return Result<ProjectWithUsersResponse>.Failure($"Project Not found. ({request.id}) ");
            }
            if (!project.Users.Any(x => x.Id == request.userid))
            {
                
                await _projectRepository.AddUserToProjectAsync(request.id, request.userid);
            }
                return Result<ProjectWithUsersResponse>.Success(_mapper.Map<ProjectWithUsersResponse>(project));
        }
    }
}
