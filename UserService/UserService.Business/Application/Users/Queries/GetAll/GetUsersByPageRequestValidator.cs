using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserService.Business.Application.Users.Queries.GetAll
{
    public class GetUsersByPageRequestValidator : AbstractValidator<GetUsersByPageRequest>
    {
        public GetUsersByPageRequestValidator()
        {

            RuleFor(x => x.pageIndex).NotEmpty();
            RuleFor(x => x.pageSize).NotEmpty();
        }
    }
}
