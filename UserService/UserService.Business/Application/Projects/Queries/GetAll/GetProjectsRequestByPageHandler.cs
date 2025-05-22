using MapsterMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UserService.DataAccess.Common;
using UserService.DataAccess.Persistence.Repositories;

namespace UserService.Business.Application.Projects.Queries.GetAll
{
    public class GetProjectsRequestByPageHandler : IRequestHandler<GetProjectsRequestByPage, PaginationResponse<ProjectResponse>>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IMapper _mapper;
        public GetProjectsRequestByPageHandler(IProjectRepository projectRepsitory,IMapper mapper) { 
            _projectRepository = projectRepsitory;
            _mapper = mapper;
        }
        public async Task<PaginationResponse<ProjectResponse>> Handle(GetProjectsRequestByPage request, CancellationToken cancellationToken)
        {
           
            var result = await _projectRepository.GetAllAsync(request.pageIndex,request.pageSize,request.userid);
            
            var projects=_mapper.Map<List<ProjectResponse>>(result.Data);

            return new PaginationResponse<ProjectResponse>(projects, result.TotalCount, result.CurrentPage, result.PageSize);
        }
    }

   


}
