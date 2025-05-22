using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserService.Business.Application.Projects.Queries.GetAll
{
    public class GetProjectRequestByPageValidator: AbstractValidator<GetProjectsRequestByPage>
    {
        public GetProjectRequestByPageValidator()
        {
                RuleFor(x=>x.pageIndex).NotEmpty();
                RuleFor(x => x.pageSize).NotEmpty();
                
        }
    }
}
