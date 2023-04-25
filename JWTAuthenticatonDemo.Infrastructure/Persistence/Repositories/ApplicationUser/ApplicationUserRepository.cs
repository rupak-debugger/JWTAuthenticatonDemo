using JWTAuthenticatonDemo.Application.Contracts.Repositories;
using JWTAuthenticatonDemo.Domain.Entities;
using JWTAuthenticatonDemo.Infrastructure.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace JWTAuthenticatonDemo.Infrastructure.Persistence.Repositories
{
    public class ApplicationUserRepository: Repository<ApplicationUser>, IApplicationUserRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public ApplicationUserRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApplicationUser> FindByEmailAsync(Expression<Func<ApplicationUser,bool>> predicate)
        {
            var user = await _dbContext.ApplicationUsers.FindAsync(predicate);
            if (user != null)
            {
                return user;
            }
            return null;
        }
    }
}
