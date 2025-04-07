using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.Business.Application.Projects;
using UserService.Business.Application.Projects.Commands.UpdateProject;
using UserService.Business.Application.Users;
using UserService.DataAccess.Entities;

namespace UserService.Business.Common.Mapping
{
    public class UserMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<User, UserResponse>()
                .Map(dest=>dest.Projects,src=> src.UserProjects.Select(x=>x.Project));

                
            

        }
    }
}
