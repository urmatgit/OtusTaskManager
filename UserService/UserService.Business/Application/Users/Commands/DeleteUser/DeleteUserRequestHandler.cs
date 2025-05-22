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
    public class DeleteUserRequestHandler : IRequestHandler<DeleteUserRequest, Result<bool>>
    {
        private readonly IProjectRepository _projectRepository;
        
        public DeleteUserRequestHandler(IProjectRepository projectRepsitory)
        {
            _projectRepository = projectRepsitory;
        
        }

        public async Task<Result<bool>> Handle(DeleteUserRequest request, CancellationToken cancellationToken)
        {
            var projectExist = await _projectRepository.GetAsync(request.id, cancellationToken);
            if (projectExist == null)
            {
                
                return Result<bool>.Failure($"Project  not found. ({request.id}) ");
            }
             _projectRepository.Delete(projectExist);
            await _projectRepository.SaveChangesAsync();
            return Result<bool>.Success(true);


        }
    }
    public class DeleteUserRequestValidator : AbstractValidator<DeleteUserRequest> 
    {
        public DeleteUserRequestValidator()
        {
            RuleFor(x => x.id).NotEmpty();
        }
    }
}
