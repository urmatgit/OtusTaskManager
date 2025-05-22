using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserService.Business.Application.Users.Commands.ManageUserProjects
{
    public class AddPpojectToUserRequesValidator: AbstractValidator<AddPpojectToUserReques>
    {
        public AddPpojectToUserRequesValidator()
        {
            RuleFor(x=>x.projectId).NotEmpty();
        }
    }
}
