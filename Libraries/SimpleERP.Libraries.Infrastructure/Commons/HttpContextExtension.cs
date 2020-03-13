using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;

namespace SimpleERP.Libraries.Infrastructure.Commons
{
    public static class HttpContextExtension
    {
        public static string GetUserName(this HttpContext httpContext)
        {
            string token = httpContext.Request.Headers["Authorization"];
            if (!string.IsNullOrEmpty(token))
            {
                JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
                JwtSecurityToken jwtToken = (JwtSecurityToken)tokenHandler.ReadToken(token);
                if (jwtToken != null)
                {
                    var subClaim = jwtToken.Claims.FirstOrDefault(o => o.Type == JwtRegisteredClaimNames.Sub);
                    if (subClaim != null)
                    {
                        string userName = subClaim.Value;
                        if (userName != null)
                        {
                            return userName.ToLower();
                        }
                    }
                }
            }
            return null;
        }
    }
}
