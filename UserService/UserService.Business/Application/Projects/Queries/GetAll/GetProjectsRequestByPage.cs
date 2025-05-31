using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UserService.DataAccess.Common;

namespace UserService.Business.Application.Projects.Queries.GetAll
{
    public record GetProjectsRequestByPage(int pageIndex ,int pageSize, Guid? userid=null): IRequest<PaginationResponse<ProjectResponse>>;
    
}
