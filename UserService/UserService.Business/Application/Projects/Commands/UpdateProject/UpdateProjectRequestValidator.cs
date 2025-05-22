using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.Business.Application.Projects.Commands.CreateProject;

namespace UserService.Business.Application.Projects.Commands.UpdateProject
{
    public class UpdateProjectRequestValidator : AbstractValidator<UpdateProjectRequest>
    {
        public UpdateProjectRequestValidator() 
        {
            RuleFor(x => x.name).NotEmpty().MaximumLength(100);

        }
    }
}
