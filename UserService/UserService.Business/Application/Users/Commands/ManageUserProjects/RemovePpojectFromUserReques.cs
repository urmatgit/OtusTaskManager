using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserService.Business.Application.Users.Commands.ManageUserProjects
{
   public record RemovePpojectFromUserReques(Guid? userId,Guid projectId): AddPpojectToUserReques(userId,projectId);


}
