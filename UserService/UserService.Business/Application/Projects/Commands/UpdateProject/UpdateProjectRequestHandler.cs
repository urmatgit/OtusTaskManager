using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.DataAccess.Common;
using UserService.DataAccess.Entities;
using UserService.DataAccess.Persistence.Repositories;

namespace UserService.Business.Application.Projects.Commands.UpdateProject
{
    public class UpdateProjectRequestHandler : IRequestHandler<UpdateProjectRequest, Result<ProjectResponse>>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IMapper _mapper;
        public UpdateProjectRequestHandler(IProjectRepository projectRepository,IMapper mapper)
        {
            _projectRepository = projectRepository;
            _mapper = mapper;
        }
        public async Task<Result<ProjectResponse>> Handle(UpdateProjectRequest request, CancellationToken cancellationToken)
        {

            var projectExist = await _projectRepository.GetAsync(request.id, cancellationToken);
            if (projectExist == null) {
                //TODO not found
                return Result<ProjectResponse>.Failure($"Project not found. ({request.id})");
            }

            projectExist.Update(request.name, request.userid);

               _projectRepository.Update(projectExist);
            await _projectRepository.SaveChangesAsync();

            return Result < ProjectResponse >.Success(_mapper.Map<ProjectResponse>(projectExist));  //new ProjectResponse(projectExist.Id, projectExist.Name, projectExist.Created, projectExist.UserId);
        }
    }
}
