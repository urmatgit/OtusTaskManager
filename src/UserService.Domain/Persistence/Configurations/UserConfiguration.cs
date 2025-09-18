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
    public class UserConfiguration : BaseEntityConfig<User>
    {
        
        public override void Configure(EntityTypeBuilder<User> builder)
        {
            base.Configure(builder);
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x=>x.UserName).IsRequired();
            builder.Property(x => x.FirstName).IsRequired().HasMaxLength(50);
            builder.Property(x => x.LastName).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Patronymic).HasMaxLength(50);
            builder.Property(x => x.Role)
                .HasConversion<string>(
                    r=>r.ToString(),
                    r=>(UserRole)Enum.Parse(typeof(UserRole), r))
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
