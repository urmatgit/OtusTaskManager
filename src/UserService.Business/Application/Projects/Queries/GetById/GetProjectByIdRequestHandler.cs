using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.DataAccess.Common;
using UserService.DataAccess.Persistence.Repositories;

namespace UserService.Business.Application.Projects.Queries.GetById
{
    public class GetProjectByIdRequestHandler : IRequestHandler<GetProjectByIdRequest, Result< ProjectResponse>>
    {
        private readonly IProjectRepository _projectRepository;

         

        public GetProjectByIdRequestHandler(IProjectRepository projectRepsitory)
        {
             _projectRepository = projectRepsitory;
        }
        public async Task<Result<ProjectResponse>> Handle(GetProjectByIdRequest request, CancellationToken cancellationToken)
        {
            var project = await _projectRepository.GetByIdAsync(request.id);
            if (project == null) {
                return Result<ProjectResponse>.Failure($"Project Not found. ({request.id}) ");
            }
            return Result < ProjectResponse >.Success( new ProjectResponse(project.Id, project.Name, project.Created, project.CreatorId));
        }
    }
}
