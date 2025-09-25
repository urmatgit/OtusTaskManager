using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.Business.Services.Auth;
using UserService.DataAccess.Common;
using UserService.DataAccess.Enums;
using UserService.DataAccess.Persistence;
using UserService.DataAccess.Persistence.Repositories.Auth;

namespace UserService.Business.Application.Users.Queries.GetById
{
    /// <summary>
    /// получает пользователя с проектами 
    /// </summary>
    public class GetUserProfileRequestHander : IRequestHandler<GetUserProfileRequest, Result<UserResponse>>
    {
        
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly ICurrentUser _currentUser;
        
        public GetUserProfileRequestHander(IUserRepository userRepository,  IMapper mapper,ICurrentUser currentUser)
        {
           
            _userRepository = userRepository;
            _mapper = mapper;
            _currentUser = currentUser;
        }
        public async Task<Result<UserResponse>> Handle(GetUserProfileRequest request, CancellationToken cancellationToken)
        {
            Guid? userid = request.userId;
            if (userid ==default(Guid?))
                userid = _currentUser.GetUserId();
            else if (request.userId != _currentUser.GetUserId() || _currentUser.GetUserRole() != UserRole.Admin)
                return Result<UserResponse>.Failure("Доступ закрыть!");
            var users = _userRepository.Get(userid.Value);
            var result=_mapper.Map<UserResponse>(users);
            return Result<UserResponse>.Success(result);
        }
    }
}
