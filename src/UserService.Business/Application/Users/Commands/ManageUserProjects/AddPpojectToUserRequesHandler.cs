using MapsterMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;
using UserService.Business.Services.Auth;
using UserService.DataAccess.Common;
using UserService.DataAccess.Common.Errors;
using UserService.DataAccess.Persistence;
using UserService.DataAccess.Persistence.Repositories.Auth;

namespace UserService.Business.Application.Users.Commands.ManageUserProjects
{
    public class AddPpojectToUserRequesHandler : IRequestHandler<AddPpojectToUserReques, Result<UserResponse>>
    {
        
        private readonly IMapper _mapper;
        private readonly ICurrentUser _currentUser;
        private readonly IUserRepository _userRepository;
        public AddPpojectToUserRequesHandler(IMapper mapper,ICurrentUser currentUser,IUserRepository userRepository)
        {
            
            _mapper = mapper;
            _currentUser = currentUser;
            _userRepository = userRepository;
        }
        public async Task<Result<UserResponse>> Handle(AddPpojectToUserReques request, CancellationToken cancellationToken)
        {
            var userId = request.projectId;
            //если не указан, тогда текущий юзер
            if (userId == null) {
                userId =  _currentUser.GetUserId();
            }
            var user = await _userRepository.GetAsync (userId,cancellationToken);
            if (user == null)
                return Result<UserResponse>.Failure(string.Format(Errors.EntityNotFound, "User", userId));

            var result = await _userRepository.AddProjectToUser(user, request.projectId);
            
            return Result<UserResponse>.Success(_mapper.Map<UserResponse>(result));
        }
    }
}
