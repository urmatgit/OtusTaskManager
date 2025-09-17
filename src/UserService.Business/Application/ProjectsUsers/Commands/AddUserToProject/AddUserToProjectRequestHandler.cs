using Mapster;
using MapsterMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.Business.Application.Projects;
using UserService.Business.Application.Projects.Commands.UpdateProject;
using UserService.Business.Application.Users;
using UserService.DataAccess.Common;
using UserService.DataAccess.Persistence.Repositories;

namespace UserService.Business.Application.ProjectsUsers.Commands.AddUserToProject
{
    public class AddUserToProjectRequestHandler : IRequestHandler<AddUserToProjectRequest, Result<List<UserResponse>>>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IMapper _mapper;
        public AddUserToProjectRequestHandler(IProjectRepository projectRepository, IMapper mapper)
        {
            _projectRepository = projectRepository;
            _mapper = mapper;
        }
        public async Task<Result<List<UserResponse>>> Handle(AddUserToProjectRequest request, CancellationToken cancellationToken)
        {
            var project = await _projectRepository.GetByIdWithUsersAsync(request.id);
            if (project == null)
            {
                return Result<List<UserResponse>>.Failure($"Project Not found. ({request.id}) ");
            }
            if (!project.Users.Any(x => x.Id == request.userid))
            {

                project= await _projectRepository.AddUserToProjectAsync(request.id, request.userid);
            }
            try
            {
                //var usersDto = new List<UserResponse>();
                //foreach(var user in project.Users)
                //{
                //    usersDto.Add(user.Adapt<UserResponse>());
                //}
                
                return Result<List<UserResponse>>.Success(project.Users.Adapt<List<UserResponse>>());
            }
            catch (Exception ex) {
                return Result<List<UserResponse>>.Failure(ex.Message);
            }
                
        }
    }
}
