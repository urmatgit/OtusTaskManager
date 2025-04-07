using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.DataAccess.Common;
using UserService.DataAccess.Persistence;
using UserService.DataAccess.Persistence.Repositories.Auth;

namespace UserService.Business.Application.Users.Queries
{
    /// <summary>
    /// получает пользователя с проектами 
    /// </summary>
    public class GetUserWithProjectsRequestHander : IRequestHandler<GetUserWithProjectsRequest, Result<UserResponse>>
    {
        private readonly TaskboardDbContext _db;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public GetUserWithProjectsRequestHander(IUserRepository userRepository, TaskboardDbContext taskboardDbContext,IMapper mapper)
        {
           _db = taskboardDbContext;
            _userRepository = userRepository;
            _mapper = mapper;
        }
        public async Task<Result<UserResponse>> Handle(GetUserWithProjectsRequest request, CancellationToken cancellationToken)
        {
            var users =await  _db.Users
                        .AsNoTracking()
                        .Include(u => u.UserProjects)
                        .ThenInclude(p => p.Project)
                        .Where(u => !u.IsDeleted && u.Id == request.userId && u.UserProjects.Any(p => !p.IsDeleted))
                        .ToListAsync();
            var result=_mapper.Map<UserResponse>(users);
            return Result<UserResponse>.Success(result);
        }
    }
}
