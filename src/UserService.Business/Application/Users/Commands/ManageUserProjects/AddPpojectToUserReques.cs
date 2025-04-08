using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.DataAccess.Common;

namespace UserService.Business.Application.Users.Commands.ManageUserProjects
{
    /// <summary>
    /// Добавляем проект в заданный user, если user не задан тогда в текущий юзер
    /// </summary>
    /// <param name="userid"></param>
    /// <param name="projectId"></param>
    public record AddPpojectToUserReques(Guid? userid, Guid projectId):IRequest<Result<UserResponse>>;
    
}
