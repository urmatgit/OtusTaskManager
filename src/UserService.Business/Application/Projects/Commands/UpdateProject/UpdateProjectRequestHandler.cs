using MapsterMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.DataAccess.Entities;
using UserService.DataAccess.Persistence.Repositories;

namespace UserService.Business.Application.Projects.Commands.UpdateProject
{
    public class UpdateProjectRequestHandler : IRequestHandler<UpdateProjectRequest, ProjectResponse>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IMapper _mapper;
        public UpdateProjectRequestHandler(IProjectRepository projectRepository,IMapper mapper)
        {
            _projectRepository = projectRepository;
            _mapper = mapper;
        }
        public async Task<ProjectResponse> Handle(UpdateProjectRequest request, CancellationToken cancellationToken)
        {

            var projectExist = await _projectRepository.GetByIdAsync(request.id);
            if (projectExist == null) {
                //TODO not found
                return null;
            }

            projectExist.Update(request.name, request.userid);

             await _projectRepository.UpdateAsync(projectExist);
            await _projectRepository.SaveChangesAsync();

            return _mapper.Map<ProjectResponse>(projectExist);  //new ProjectResponse(projectExist.Id, projectExist.Name, projectExist.Created, projectExist.UserId);
        }
    }
}
