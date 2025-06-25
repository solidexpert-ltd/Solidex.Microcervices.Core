using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Solidex.Core.Base.ComplexTypes;

public class AuthOptions
{
    public string JwtKey { get; set; }
    public string JwtIssuer { get; set; }
}

namespace Solidex.Microservices.Core.JwtAuth
{
    public static class JwtTokenProvider
    {
        public static string GenerateJwtToken(string userName, string userId, Guid ui, IList<string> roles, AuthOptions authOptions)
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

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authOptions.JwtKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(30);

            var token = new JwtSecurityToken(
                authOptions.JwtIssuer,
                authOptions.JwtIssuer,
                claims,
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


        public static string GenerateSystemToken(SystemUsersEnumeration user, AuthOptions authOptions)
        {
            return GenerateJwtToken("System", Guid.Empty.ToString(),
                user.Identificator,
                new List<string>
                {
                    "root-admin",
                    "root-user",
                    "category-administrator"
                }, authOptions);
        }
    }
}