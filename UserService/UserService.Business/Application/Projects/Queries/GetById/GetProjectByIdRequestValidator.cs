using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserService.Business.Application.Projects.Queries.GetById
{
    public class GetProjectByIdRequestValidator: AbstractValidator<GetProjectByIdRequest>
    {
        public GetProjectByIdRequestValidator()
        {
            RuleFor(x=>x.id).NotEmpty();
        }
    }
}
