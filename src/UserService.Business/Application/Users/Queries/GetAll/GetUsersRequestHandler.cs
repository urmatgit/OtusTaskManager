using MapsterMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.Business.Application.Projects;
using UserService.Business.Services.Radis;
using UserService.DataAccess.Common;
using UserService.DataAccess.Persistence;
using UserService.DataAccess.Persistence.Repositories.Auth;

namespace UserService.Business.Application.Users.Queries.GetAll
{
    public class GetUsersRequestHandler : IRequestHandler<GetUsersRequest, List<UserResponse>>
    {
        
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly ICacheService _cacheService;
        public GetUsersRequestHandler(
            IUserRepository userRepository,
            IMapper mapper,
            ICacheService cacheService
            ) { 
            _userRepository = userRepository;
            _mapper = mapper;
            _cacheService = cacheService;
        }
        public async Task<List<UserResponse>> Handle(GetUsersRequest request, CancellationToken cancellationToken)
        {
            var usersCached =await  _cacheService.GetAsync<List<UserResponse>>(CacheKeys.AllUsers);
            if (usersCached != null)
            {
                return usersCached;
            }
            
            var result = await _userRepository.GetAllAsync(cancellationToken,asNoTracking: true);

            var users = _mapper.Map<List<UserResponse>>(result);

            // Сохранение в кэш с настройками
            var cacheOptions = CacheProfiles.ShortLived.ToDistributedCacheEntryOptions();

            await _cacheService.SetAsync<List<UserResponse>>(CacheKeys.AllUsers, users, cacheOptions);
            return users;
        }
    }
}
