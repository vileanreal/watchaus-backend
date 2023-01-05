using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WH.ADMIN.Models.Token;

namespace WH.ADMIN.Helper
{
    public static class TokenHelper
    {

        public static string Issuer { get; set; }
        public static string Audience { get; set; }
        public static string Key { get; set; }


        public static string GenerateToken(UserDetails user)
        {
            var issuer = Issuer;
            var audience = Audience;
            var key = Encoding.ASCII.GetBytes(Key);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                new Claim("id", Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, user.Username),
                new Claim("userId", user.UserId),
                new Claim("username", user.Username),
                new Claim("roleId", user.RoleId),
                new Claim("roleName", user.RoleName),
                new Claim(JwtRegisteredClaimNames.Email, user.Email)
             }),
                Expires = DateTime.UtcNow.AddMinutes(5),
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var stringToken = tokenHandler.WriteToken(token);
            return stringToken;
        }

    }



}
