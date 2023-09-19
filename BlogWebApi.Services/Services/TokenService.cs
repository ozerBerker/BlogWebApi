using BlogWebApi.Contracts.Interfaces;
using BlogWebApi.Core.Models;
using BlogWebApi.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace BlogWebApi.Services.Services
{
    public class TokenService : ITokenService
    {
        private IConfiguration configuration { get; } // appsettings dosyasından okumak için
        private readonly Core.Models.TokenOptions TokenOptions;
        private readonly DateTime accessTokenExpiration;
        private readonly UserManager<AppUser> userManager;

        public TokenService(IConfiguration configuration)
        {
            this.configuration = configuration;
            this.TokenOptions = new Core.Models.TokenOptions()
            {
                Audience = configuration["JWTSettings:Audience"],
                Issuer = configuration["JWTSettings:Issuer"],
                AccessTokenExpiration = int.Parse(configuration["JWTSettings:AccessTokenExpiration"]),
                SecurityKey = configuration["JWTSettings:SecurityKey"]
            };
            accessTokenExpiration = DateTime.Now.AddMinutes(TokenOptions.AccessTokenExpiration);
        }

        public AccessTokenModel CreateToken(AppUser user, Task<IList<string>> roles)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(TokenOptions.SecurityKey));
            var signingCredential = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
            var jwt = new JwtSecurityToken(
                issuer: TokenOptions.Issuer,
                audience: TokenOptions.Audience,
                expires: accessTokenExpiration,
                notBefore: DateTime.Now,
                claims: SetClaims(user, roles),
                signingCredentials: signingCredential
                );

            var token = new JwtSecurityTokenHandler().WriteToken(jwt);
            return new AccessTokenModel()
            {
                Token = token,
                Expiration = accessTokenExpiration
            };
        }

        private IEnumerable<Claim> SetClaims(AppUser user, Task<IList<string>> roles)
        {
            var claims = new List<Claim>();
            roles.Result.Select(x => x).ToList().ForEach(role => claims.Add(new Claim("roles", role)));
            claims.Add(new Claim("role", "Admin"));
            claims.Add(new Claim("email", user.Email));
            claims.Add(new Claim("id", user.Id.ToString()));
            claims.Add(new Claim("name", user.UserName));
            return claims;
        }
    }
}

