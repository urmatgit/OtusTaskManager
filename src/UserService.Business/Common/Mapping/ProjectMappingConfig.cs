using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.Business.Application.Projects;
using UserService.Business.Application.Projects.Commands.UpdateProject;
using UserService.DataAccess.Entities;

namespace UserService.Business.Common.Mapping
{
    public class ProjectMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Project,ProjectResponse>()
                .Map(dest=>dest.owner,src=>src.UserId);
            config.NewConfig<UpdateProjectRequest, Project>();
            

        }
    }
}
