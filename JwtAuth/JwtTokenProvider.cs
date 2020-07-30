using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microcervices.Core.Infrasructure.Authorization;
using Microsoft.IdentityModel.Tokens;
using Solidex.Core.Base.ComplexTypes;

namespace Microcervices.Core.JwtAuth
{
    public static class JwtTokenProvider
    {
        public static string GenerateJwtToken(string userName, string userId, Guid ui, IList<string> roles)
        {
            var claims = new List<Claim>
            {
                new Claim("UserInformationID", ui.ToString()),
                new Claim(ClaimTypes.Name, userName),
                new Claim(ClaimTypes.NameIdentifier, userId)
            };

            foreach (string role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AuthOptions.JwtKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(30);

            var token = new JwtSecurityToken(
                AuthOptions.JwtIssuer,
                AuthOptions.JwtIssuer,
                claims,
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public static string GenerateSystemToken()
        {
            return GenerateSystemToken(SystemUsersEnumeration.SystemMessage);
        }

        public static string GenerateSystemToken(SystemUsersEnumeration user)
        {
            return GenerateJwtToken("System", Guid.Empty.ToString(),
                user.Identificator,
                new List<string>
                {
                    "root-admin",
                    "root-user",
                    "category-administrator"
                });
        }
    }
}