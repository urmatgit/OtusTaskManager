using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.Business.Application.Users;
using UserService.Business.Services.Radis;
using UserService.DataAccess.Common;
using UserService.DataAccess.Persistence.Repositories;

namespace UserService.Business.Application.Projects.Queries.GetById
{
    //получаем пользователей проекта
    public class GetUsersByProjectRequestHandler : IRequestHandler<GetUsersByProjectRequest, Result<List<UserResponse>>
    { 
        private readonly IProjectRepository _projectRepository;
        private readonly IMapper _mapper;
        private readonly ICacheService _cacheService; 

        public GetUsersByProjectRequestHandler(IProjectRepository projectRepsitory,IMapper mapper,ICacheService cacheService)
        {
             _projectRepository = projectRepsitory;
            _mapper = mapper;
            _cacheService = cacheService;
        }
        public async Task<Result<List<UserResponse>>> Handle(GetUsersByProjectRequest request, CancellationToken cancellationToken)
        {
            var chacheUsersInProject = await _cacheService.GetAsync<List<UserResponse>>($"{CacheKeys.ProjectUsersById}_{request.id}");
            if (chacheUsersInProject != null)
            {
                return Result<List<UserResponse>>.Success(chacheUsersInProject);
            }
            var project = await _projectRepository.GetByIdWithUsersAsync(request.id);
            if (project == null) {
                return Result<List<UserResponse>>.Failure($"Project Not found. ({request.id}) ");
            }
            var users = _mapper.Map<ProjectWithUsersResponse>(project).Users;
            var cacheOptions = CacheProfiles.ShortLived.ToDistributedCacheEntryOptions();
            await _cacheService.SetAsync<List<UserResponse>>($"{CacheKeys.ProjectUsersById}_{request.id}", users, cacheOptions);
            return Result <List<UserResponse>>.Success(users);
        }
    }
}
