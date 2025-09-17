using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.Business.Application.Users;
using UserService.Business.Common.Interfaces;
using UserService.DataAccess.Entities;

namespace UserService.Business.Application.Projects
{
    public record ProjectResponse(Guid id,string name,DateTime created,Guid owner, ICollection<User>? Users) : IDto;
    
}
