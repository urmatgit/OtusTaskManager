using MapsterMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.Business.Services.Radis;
using UserService.DataAccess.Common;
using UserService.DataAccess.Persistence.Repositories.Auth;

namespace UserService.Business.Application.Users.Commands.EditUser
{
    public class ChangeUserRoleRequestHandler : IRequestHandler<ChangeUserRoleRequest, Result<UserResponse>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly ICacheService _cacheService;
        public ChangeUserRoleRequestHandler(IUserRepository userRepository,IMapper mapper,ICacheService cacheService)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _cacheService = cacheService;
        }
        public async Task<Result<UserResponse>> Handle(ChangeUserRoleRequest request, CancellationToken cancellationToken)
        {
            await _cacheService.RemoveAsync(CacheKeys.AllUsers, cancellationToken);
            var user = await _userRepository.GetAsync(request.userid,cancellationToken);
            if (user == null) {
                return Result<UserResponse>.Failure($"User not found. {request.userid}");
            }
            user.Update(role:request.newRole);
             _userRepository.Update(user);
            await _userRepository.SaveChangesAsync();

            return Result<UserResponse>.Success(_mapper.Map<UserResponse>(user));
        }
    }
}
