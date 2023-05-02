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
    public class LoginTokenRepository : Repository<LoginToken>, ILoginTokenRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public LoginTokenRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<LoginToken> FirstOrDefaultAsync(Expression<Func<LoginToken, bool>> predicate)
        {
            var loginToken = await Task.Run(() =>
            _dbContext.LoginTokens.FirstOrDefault(predicate)
            );
            return loginToken;
        }
    }
}
