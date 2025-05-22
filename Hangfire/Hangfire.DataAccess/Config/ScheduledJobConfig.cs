using Hangfire.DataAccess.Entities;

using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hangfire.DataAccess.Config
{
    public class ScheduledJobConfig : BaseEntityConfig<ScheduledJob>
    {
        public override void Configure(EntityTypeBuilder<ScheduledJob> builder)
        {
            base.Configure(builder);

            builder.Property(t => t.CustomJobId).IsRequired(true);
            builder.Property(t => t.JobId).IsRequired(true);
            builder.Property(t => t.CreatedDateTime).IsRequired(true);
        }
    }
}