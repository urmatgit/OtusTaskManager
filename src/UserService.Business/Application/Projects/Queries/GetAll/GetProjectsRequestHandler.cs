using MapsterMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.DataAccess.Persistence.Repositories;

namespace UserService.Business.Application.Projects.Queries.GetAll
{
    //все проекты
    public class GetProjectsRequestHandler : IRequestHandler<GetProjectsRequest, List<ProjectResponse>>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IMapper _mapper;
        public GetProjectsRequestHandler(IProjectRepository projectRepsitory, IMapper mapper)
        {
            _projectRepository = projectRepsitory;
            _mapper = mapper;
        }
        public async Task<List<ProjectResponse>> Handle(GetProjectsRequest request, CancellationToken cancellationToken)
        {

            var result = await _projectRepository.GetAllAsync(cancellationToken);

            var projects = _mapper.Map<List<ProjectResponse>>(result);

            return projects;
        }
    }
}
