using MapsterMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.DataAccess.Common;
using UserService.DataAccess.Persistence.Repositories.Auth;

namespace UserService.Business.Application.Users.Commands.EditUser
{
    public class ChangeUserRoleRequestHandler : IRequestHandler<ChangeUserRoleRequest, Result<UserResponse>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public ChangeUserRoleRequestHandler(IUserRepository userRepository,IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
        public async Task<Result<UserResponse>> Handle(ChangeUserRoleRequest request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.userid);
            if (user == null) {
                return Result<UserResponse>.Failure($"User not found. {request.userid}");
            }
            user.Role = request.Role;
            await _userRepository.UpdateAsync(user);
            await _userRepository.SaveChangesAsync();

            return Result<UserResponse>.Success(_mapper.Map<UserResponse>(user));
        }
    }
}
