using FluentValidation;
using Mapster;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.Business.Services.Auth;
using UserService.DataAccess.Common;
using UserService.DataAccess.Entities;
using UserService.DataAccess.Persistence.Repositories;

namespace UserService.Business.Application.Projects.Commands.DeleteProject
{
    public class DeleteProjectRequestHandler : IRequestHandler<DeleteProjectRequest, Result<bool>>
    {
        private readonly IProjectRepository _projectRepository;
        
        public DeleteProjectRequestHandler(IProjectRepository projectRepsitory)
        {
            _projectRepository = projectRepsitory;
        
        }

        public async Task<Result<bool>> Handle(DeleteProjectRequest request, CancellationToken cancellationToken)
        {
            var projectExist = await _projectRepository.GetByIdAsync(request.id);
            if (projectExist == null)
            {
                
                return Result<bool>.Failure($"Project  not found. ({request.id}) ");
            }
             await _projectRepository.DeleteAsync(projectExist);
            await _projectRepository.SaveChangesAsync();
            return Result<bool>.Success(true);


        }
    }
    public class DeleteProjectRequestValidator : AbstractValidator<DeleteProjectRequest> 
    {
        public DeleteProjectRequestValidator()
        {
            RuleFor(x => x.id).NotEmpty();
        }
    }
}
