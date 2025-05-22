using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserService.Business.Application.Projects.Commands.CreateProject
{
    public class CreateProjectRequestValidator: AbstractValidator<CreateProjectRequest>
    {
        public CreateProjectRequestValidator() { 
            RuleFor(x=>x.name).NotEmpty().MaximumLength(100);
            //RuleFor(x => x.UserId).NotNull().NotEmpty();
            
        }
    }
}
