using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
    public class Session
    {
        public long Id { get; set; }
        public string Username { get; set; }
        public long RoleId { get; set; }
        public string RoleDesc { get; set; }
        public string Email { get; set; }

        // Pass HttpContext.User
        public Session(ClaimsPrincipal principal) {
            IEnumerable<Claim> claims = principal.Claims;
            foreach (Claim claim in claims)
            {
                switch (claim.Type)
                {
                    case "userId":
                        this.Id = Int64.Parse(claim.Value);
                        break;
                    case "username":
                        this.Username = claim.Value;
                        break;
                    case "email":
                        this.Email = claim.Value;
                        break;
                    case "roleId":
                        this.RoleId = Int64.Parse(claim.Value);
                        break;
                    case "roleName":
                        this.RoleDesc = claim.Value;
                        break;
                }
            }
        }
    }
}
