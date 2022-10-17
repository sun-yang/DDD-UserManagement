using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserMgmt.Domain.Entities;

namespace UserMgmt.Infrastructure.Configs
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("T_Users");
            builder.OwnsOne(x => x.PhoneNumber, nb =>
            {
               nb.Property(x => x.Number).HasMaxLength(20).IsUnicode(false);
            });
            builder.Property("PasswordHash").HasMaxLength(100).IsUnicode(false);
            builder.HasOne(x => x.UserAccessFail).WithOne(f => f.User).HasForeignKey<UserAccessFail>(f => f.UserId);
        }

    }
}
