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
    public class ProjectConfiguration : BaseEntityConfig<Project>
    {
        public override void Configure(EntityTypeBuilder<Project> builder)
        {
            base.Configure(builder);
            builder.Property(x => x.Name).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Created).IsRequired();
            builder.Property(x=>x.CreatorId).IsRequired();
            //many-to-many
            builder.HasMany(p => p.Users)
                .WithMany(up => up.Projects).UsingEntity<UserProject>(
                    left => left.HasOne<User>().WithMany().HasForeignKey(up => up.UserId).IsRequired(),
                    right => right.HasOne<Project>().WithMany().HasForeignKey(up => up.ProjectId).IsRequired()
                );

            
        }
    }
}
