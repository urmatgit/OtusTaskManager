using Hangfire.DataAccess.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hangfire.DataAccess.Config
{
    public abstract class BaseEntityConfig<T> : IEntityTypeConfiguration<T> where T : BaseEntity
    {
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            builder.HasKey(t => t.Id);
            builder.Property(t => t.IsDeleted).IsRequired(true);
        }
    }
}