using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.DataAccess.Entities;
using UserService.DataAccess.Enums;

namespace UserService.DataAccess.Persistence.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.FirstName).IsRequired().HasMaxLength(50);
            builder.Property(x => x.LastName).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Patronymic).HasMaxLength(50);
            builder.Property(x => x.Role)
                .HasConversion<string>(
                    r=>r.ToString(),
                    r=>(ProjectRole)Enum.Parse(typeof(ProjectRole), r))
                .IsRequired();
            builder.Property(x=>x.Status)
                .HasConversion<string>(
                    t=>t.ToString(),
                    t=>(Status)Enum.Parse(typeof(Status),t))
                .IsRequired();
            builder.Property(x => x.Email).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Phone).IsRequired().HasMaxLength(20);
            
            
        }
    }
}
