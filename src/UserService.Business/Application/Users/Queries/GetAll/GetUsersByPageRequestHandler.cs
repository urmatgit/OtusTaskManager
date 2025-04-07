using MapsterMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.DataAccess.Common;
using UserService.DataAccess.Persistence;

namespace UserService.Business.Application.Users.Queries.GetAll
{
    public class GetUsersByPageRequestHandler : IRequestHandler<GetUsersByPageRequest, PaginationResponse<UserResponse>>
    {
        private readonly TaskboardDbContext _dbContext;
        private readonly IMapper _mapper;
        public GetUsersByPageRequestHandler(
            TaskboardDbContext taskboardDbContext,
            IMapper mapper
            ) { 
            _dbContext = taskboardDbContext;
            _mapper = mapper;
        }
        public Task<PaginationResponse<UserResponse>> Handle(GetUsersByPageRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
