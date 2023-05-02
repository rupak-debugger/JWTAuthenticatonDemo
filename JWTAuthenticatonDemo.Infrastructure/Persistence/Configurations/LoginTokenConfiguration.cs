using JWTAuthenticatonDemo.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JWTAuthenticatonDemo.Infrastructure.Persistence.Configurations
{
    public class LoginTokenConfiguration : IEntityTypeConfiguration<LoginToken>
    {
        public void Configure(EntityTypeBuilder<LoginToken> builder)
        {
            builder.HasOne(t => t.User).WithMany(u => u.LoginTokens).HasForeignKey(t => t.UserId);
            builder.Property(a => a.CreatedBy).HasMaxLength(50);
            builder.Property(a => a.LastModifiedBy).HasMaxLength(50);
        }
    }
}
