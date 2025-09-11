using FluentValidation;
using Mapster;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.Business.Services.Auth;
using UserService.Business.Services.Radis;
using UserService.DataAccess.Common;
using UserService.DataAccess.Entities;
using UserService.DataAccess.Persistence.Repositories;
using UserService.DataAccess.Persistence.Repositories.Auth;

namespace UserService.Business.Application.Projects.Commands.DeleteProject
{
    public class DeleteUserRequestHandler : IRequestHandler<DeleteUserRequest, Result<bool>>
    {
        private readonly IUserRepository _projectRepository;
        private readonly ICacheService _cacheService;
        public DeleteUserRequestHandler(IUserRepository projectRepsitory,ICacheService cacheService)
        {
            _projectRepository = projectRepsitory;
            _cacheService = cacheService;
        }

        public async Task<Result<bool>> Handle(DeleteUserRequest request, CancellationToken cancellationToken)
        {
            await _cacheService.RemoveAsync(CacheKeys.AllUsers, cancellationToken);

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
