using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Invitation_App_Web_API.Models.ViewModels;
using Microsoft.IdentityModel.Tokens;

namespace Invitation_App_Web_API.Util
{
    public static class JwtUtil
    {
        public static JwtSecurityToken CreateJwtToken(UserViewModel user)
        {
            var claims = new[]
            {
                new Claim("UserId", user.Id.ToString()),
                new Claim("ApplicationName", "Invitation App"),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.Id.ToString())

            };

            var loginKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("TayfunsSecureKey"));

            return new JwtSecurityToken(
                "Tayfun Arslan",
                "Tayfun Arslan",
                expires: DateTime.UtcNow.AddDays(7),
                claims: claims,
                signingCredentials: new SigningCredentials(loginKey, SecurityAlgorithms.HmacSha256));
        }
    }
}
