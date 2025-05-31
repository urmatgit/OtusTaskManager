using MapsterMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.Business.Application.Projects;
using UserService.DataAccess.Common;
using UserService.DataAccess.Persistence;
using UserService.DataAccess.Persistence.Repositories.Auth;

namespace UserService.Business.Application.Users.Queries.GetAll
{
    public class GetUsersByPageRequestHandler : IRequestHandler<GetUsersByPageRequest, PaginationResponse<UserResponse>>
    {
        
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        public GetUsersByPageRequestHandler(
            IUserRepository userRepository,
            IMapper mapper
            ) { 
            _userRepository = userRepository;
            _mapper = mapper;
        }
        public async Task<PaginationResponse<UserResponse>> Handle(GetUsersByPageRequest request, CancellationToken cancellationToken)
        {
            var result = await _userRepository.GetAllAsync(request.pageIndex, request.pageSize);

            var users = _mapper.Map<List<UserResponse>>(result.Data);

            return new PaginationResponse<UserResponse>(users, result.TotalCount, result.CurrentPage, result.PageSize);
        }
    }
}
