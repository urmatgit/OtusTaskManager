using Hangfire.DataAccess.Entities;

using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hangfire.DataAccess.Config
{
    public class UserConfig : BaseEntityConfig<User>
    {
        public override void Configure(EntityTypeBuilder<User> builder)
        {
            base.Configure(builder);

            builder.Property(t => t.UserName).IsRequired(true);
            builder.Property(t => t.PasswordHash).IsRequired(true);
            builder.Property(t => t.FirstName).IsRequired(true);
            builder.Property(t => t.LastName).IsRequired(true);
            builder.Property(t => t.Patronymic).IsRequired(false);
            builder.Property(t => t.Email).IsRequired(true);
            builder.Property(t => t.Phone).IsRequired(true);
            builder.Property(t => t.Status).IsRequired(true);
            builder.Property(t => t.DateReg).IsRequired(true);
        }
    }
}