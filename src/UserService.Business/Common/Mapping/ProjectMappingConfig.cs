using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
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
                .Map(dest=>dest.owner,src=>src.CreatorId);
            config.NewConfig<UpdateProjectRequest, Project>()
                .Map(dest => dest.Name, src => src.name)
                .Map(dest => dest.CreatorId, src => src.userid)
                .Map(dest => dest.Id, src => src.id);

            config.NewConfig<Project, ProjectWithUsersResponse>()
                .Map(d=>d.Users,s=>s.Users)
                .Map(dest => dest.owner, src => src.CreatorId);
                




        }
    }
}
