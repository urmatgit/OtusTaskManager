using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.DataAccess.Enums;

namespace UserService.Business.Application.Users.Commands.EditUser
{
    public class ChangeUserRoleRequestValidator :AbstractValidator<ChangeUserRoleRequest>
    {
        public ChangeUserRoleRequestValidator()
        {
            RuleFor(x=>x.userid).NotEmpty();
            RuleFor(x => x.newRole).NotEmpty()
                .Must(r => Enum.IsDefined(typeof(UserRole), r))
                .IsInEnum();
        }
    }
}
