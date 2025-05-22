using Hangfire.DataAccess.Entities;

using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hangfire.DataAccess.Config
{
    public class ProjectConfig : BaseEntityConfig<Project>
    {
        public override void Configure(EntityTypeBuilder<Project> builder)
        {
            base.Configure(builder);

            builder.Property(t => t.Name).IsRequired(true);
            builder.HasMany(t => t.Users)
                   .WithMany(t => t.Projects).UsingEntity<UserProject>(
                    left => left.HasOne<User>().WithMany().HasForeignKey(t => t.UserId).IsRequired(true),
                    right => right.HasOne<Project>().WithMany().HasForeignKey(t => t.ProjectId).IsRequired(true)
                );
        }
    }
}