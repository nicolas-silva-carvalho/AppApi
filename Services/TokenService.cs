using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ApiAPP.Models;
using Microsoft.IdentityModel.Tokens;

namespace AppApi.Services
{
    public class TokenService : IToken
    {
        private readonly IConfiguration _config;
        public TokenService(IConfiguration config)
        {
            _config = config;
        }

        public Task<string> CriarToken(UsuarioLogin usuario)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["jwt:Key"]!));
            var sign = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var jwtToken = new JwtSecurityToken(

                issuer: _config["jwt:Issuer"],
                audience: _config["jwt:Audience"],
                claims: new List<Claim>(),
                expires: DateTime.Now.AddHours(1),
                signingCredentials: sign
                );

            var token =  new JwtSecurityTokenHandler().WriteToken(jwtToken);
            return Task.FromResult(token);
        }
    }
}