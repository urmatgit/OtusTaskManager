using Hangfire.DataAccess.Entities;

using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hangfire.DataAccess.Config
{
    public class UserProjectConfig : BaseEntityConfig<UserProject>
    {
        public override void Configure(EntityTypeBuilder<UserProject> builder)
        {
            base.Configure(builder);

            builder.Property(t => t.UserId).IsRequired(true);
            builder.Property(t => t.ProjectId).IsRequired(true);
        }
    }
}