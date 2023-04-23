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
    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.Property(a => a.FirstName).HasMaxLength(20);
            builder.Property(a => a.LastName).HasMaxLength(20);
            builder.Property(a => a.Email).HasMaxLength(20);
            builder.Property(a => a.CreatedBy).HasMaxLength(50);
            builder.Property(a => a.LastModifiedBy).HasMaxLength(50);
            builder.HasIndex(a => a.Email).IsUnique();
        }
    }
}
