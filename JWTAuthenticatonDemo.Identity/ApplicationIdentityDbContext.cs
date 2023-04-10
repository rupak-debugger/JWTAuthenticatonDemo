using JWTAuthenticatonDemo.Identity.Configurations;
using JWTAuthenticatonDemo.Identity.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JWTAuthenticatonDemo.Identity
{
    public class ApplicationIdentityDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationIdentityDbContext(DbContextOptions<ApplicationIdentityDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelbuilder)
        {
            base.OnModelCreating(modelbuilder);
            modelbuilder.ApplyConfiguration(new RoleConfiguration());
            modelbuilder.ApplyConfiguration(new UserConfiguration());
            modelbuilder.ApplyConfiguration(new UserRoleConfiguration());
        }
    }
}
