using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CloudnessMarketplace.Functions.Extensions
{
    public static class HttpRequestExtensions
    {

        /// <summary>
        /// Get the user id from the access token
        /// </summary>
        /// <param name="request"></param>
        /// <returns>UserId extracted from the access token or null if it's not there</returns>
        public static string GetUserId(this HttpRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            string authorizationHeader = request.Headers["Authorization"];

            if (!string.IsNullOrWhiteSpace(authorizationHeader))
            {
                string token = authorizationHeader.Replace("Bearer ", "");
                var jwtHandler = new JwtSecurityTokenHandler();
                var accessToken = jwtHandler.ReadJwtToken(token);

                if (!accessToken.Claims.Any())
                    return null; 

                var userId = accessToken.Claims.SingleOrDefault(i => i.Type == "oid").Value;

                return userId;
            }

            return null;
        }

    }
}
