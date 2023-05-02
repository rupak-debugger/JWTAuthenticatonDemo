using JWTAuthenticatonDemo.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace JWTAuthenticatonDemo.Application.Contracts.Repositories
{
    public interface ILoginTokenRepository : IRepository<LoginToken>
    {
        Task<LoginToken> FirstOrDefaultAsync(Expression<Func<LoginToken, bool>> predicate);
    }
}
