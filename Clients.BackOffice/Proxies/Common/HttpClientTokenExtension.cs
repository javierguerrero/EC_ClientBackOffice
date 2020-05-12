using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Clients.BackOffice.Proxies.Common
{
    public static class HttpClientTokenExtension
    {
        public static void AddBearerToken(this HttpClient client, IHttpContextAccessor context)
        {
            if (context.HttpContext.User.Identity.IsAuthenticated)
            {
                var token = context.HttpContext.User.Claims.FirstOrDefault(x => x.Type.Equals("access_token"))?.Value;

                if (!string.IsNullOrEmpty(token))
                {
                    client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", "Bearer " + token);
                }
            }
        }
    }
}
