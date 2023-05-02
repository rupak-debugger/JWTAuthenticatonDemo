using JWTAuthenticatonDemo.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JWTAuthenticatonDemo.Domain.Entities
{
    public class LoginToken : AuditableBaseEntity
    {
        public int UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
        public string RefreshToken { get; set; }
        public DateTime LoggedInAt { get; set; } = DateTime.UtcNow;
    }
}
