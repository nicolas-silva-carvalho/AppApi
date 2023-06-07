using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ApiAPP.DTOs;
using ApiAPP.Models;
using ApiAPP.Services;
using AutoMapper;
using Microsoft.IdentityModel.Tokens;

namespace AppApi.Services
{
    public class TokenService : IToken
    {
        private readonly IConfiguration _config;
        private readonly IMapper _mapp;
        private readonly UsuarioService _usuarioService;
        public TokenService(IConfiguration config, IMapper mapp, UsuarioService usuarioService)
        {
            _mapp = mapp;
            _config = config;
            _usuarioService = usuarioService;
        }

        public Task<string> CriarToken(Usuario usuario)
        {
            
            var claims = new List<Claim>
             {
                new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                new Claim(ClaimTypes.Name, usuario.Nome)
             };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["jwt:Key"]!));
            var sign = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var jwtToken = new JwtSecurityToken(

                issuer: _config["jwt:Issuer"],
                audience: _config["jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: sign

                );

            var token = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            return Task.FromResult(token);
        }
    }
}