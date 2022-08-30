using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DynamicBlog.Models
{
    public class GetUserInfo
    {
        public int GetId(ClaimsPrincipal user)
        {
            return int.Parse(((ClaimsIdentity)user.Identity).FindFirst(ClaimTypes.Name).Value);
        }
        public string GetMail(ClaimsPrincipal user)
        {
            return ((ClaimsIdentity)user.Identity).FindFirst(ClaimTypes.Email).Value.ToString();
        }
    }
}