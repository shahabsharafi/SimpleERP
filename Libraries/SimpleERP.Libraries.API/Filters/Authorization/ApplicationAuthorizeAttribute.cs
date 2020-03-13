using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using SimpleERP.Libraries.Infrastructure.Commons;

namespace SimpleERP.Libraries.API.Filters
{
    public class ApplicationAuthorizeAttribute : TypeFilterAttribute
    {
        public ApplicationAuthorizeAttribute(params string[] claims) : base(typeof(AuthorizeFilter))
        {
            Arguments = new object[] { claims };
        }
    }

    public class AuthorizeFilter : IAuthorizationFilter
    {
        readonly ILogger<AuthorizeFilter> _logger;
        readonly IDistributedCache _distributedCache;
        readonly string[] _claims;

        public AuthorizeFilter(ILogger<AuthorizeFilter> logger, IDistributedCache distributedCache, params string[] claims)
        {
            _logger = logger;
            _distributedCache = distributedCache;
            _claims = claims;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            string userName = context.HttpContext.GetUserName();
            if (userName != null)
            {
                string user_climes_key = userName.ToLower() + "_claims";
                byte[] data = _distributedCache.Get(user_climes_key);
                if (data != null)
                {
                    var clims = data.FromByteArray<List<string>>();
                    if (!this._claims.Any(c => !clims.Contains(c)))
                    {
                        return;
                    }
                }
            }
            context.Result = new UnauthorizedResult();
        }

        //public void OnAuthorization(AuthorizationFilterContext context)
        //{
        //    string token = context.HttpContext.Request.Headers["Authorization"];
        //    if (!string.IsNullOrEmpty(token))
        //    {
        //        JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
        //        JwtSecurityToken jwtToken = (JwtSecurityToken)tokenHandler.ReadToken(token);
        //        if (jwtToken != null)
        //        {
        //            var subClaim = jwtToken.Claims.FirstOrDefault(o => o.Type == JwtRegisteredClaimNames.Sub);
        //            if (subClaim != null)
        //            {
        //                string userName = subClaim.Value;
        //                if (userName != null)
        //                {
        //                    string user_climes_key = userName.ToLower() + "_claims";
        //                    byte[] data = _distributedCache.Get(user_climes_key);
        //                    if (data != null)
        //                    {
        //                        var clims = data.FromByteArray<List<string>>();
        //                        if (!this._claims.Any(c => !clims.Contains(c)))
        //                        {
        //                            return;
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    context.Result = new UnauthorizedResult();
        //}
        
    }
}