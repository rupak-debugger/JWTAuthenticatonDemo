using JWTAuthenticatonDemo.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JWTAuthenticatonDemo.Application.Contracts.Repositories
{
    public interface IApplicationUserRepository : IRepository<ApplicationUser>
    {
    }
}
