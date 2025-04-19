using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.DataAccess.Entities;

namespace UserService.DataAccess.Persistence.Configurations
{
    public class UserProjectConfiguration : BaseEntityConfig<UserProject>
    {
        public override void Configure(EntityTypeBuilder<UserProject> builder)
        {
            base.Configure(builder);
            builder.Property(up => up.UserId).IsRequired();
            builder.Property(up=>up.ProjectId).IsRequired();
            builder.HasKey(up => new { up.UserId, up.ProjectId });
            //builder.HasOne(up => up.User)
            //    .WithMany(u => u.UserProjects)
            //    .HasForeignKey(u => u.UserId);
            //builder.HasOne(up=>up.Project)
            //    .WithMany(p=>p.UserProjects)
            //    .HasForeignKey(p => p.ProjectId);
        }
    }
}
