using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using ApiAPP.Data;
using ApiAPP.Models;
using ApiAPP.Services;
using AppApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
namespace ApiAPP.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class UsuarioController : ControllerBase
{
    private readonly IConfiguration _config;
    private readonly IToken _token;
    private readonly AppService _appService;
    public UsuarioController(IConfiguration config, IToken token, AppService appService)
    {
        _appService = appService;
        _token = token;
        _config = config;
    }

    [HttpPost("Login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login(UsuarioLogin usuario)
    {
        try
        {
            var usuarioNome = await _appService.PegarUsuarioPorNome(usuario.NomeUsuario);
            if(usuarioNome == null) return Unauthorized("Usuário inválido.");

            var usuarioSenha = await _appService.PegarUsuarioPorSenha(usuario.Senha);
            if(usuarioSenha == null) return Unauthorized();

            return Ok(new 
            {
                nomeUsuario  = usuario.NomeUsuario,
                emailUsuario = usuario.Senha,
                token = _token.CriarToken(usuario).Result
            });
        }
        catch (Exception ex)
        {
           return this.StatusCode(StatusCodes.Status500InternalServerError,
            $"Erro ao tentar recuperar usuários. Erro: {ex.Message}");
        }
    }

    // [HttpPost("Registrar")]
    // [AllowAnonymous]
    // public async Task<IActionResult> Registrar(UserDto usuarioDto)
    // {
    //     try
    //     {
    //         if(await _usuarioService.UsuarioExiste(usuarioDto.UserName)) return BadRequest("Usuário já existe.");

    //         var user = await _usuarioService.CriarConta(usuarioDto);
    //         if(user != null)
    //          return Ok(user);

    //         return BadRequest("Usuário não criado. Tente novamente mais tarde.");
    //     }
    //     catch (Exception ex)
    //     {
    //        return this.StatusCode(StatusCodes.Status500InternalServerError,
    //                 $"Erro ao tentar recuperar usuários. Erro: {ex.Message}");
    //     }
    // }

    // [HttpGet("id")]
    // public async Task<List<Relatorio>> GetUser(string id) =>
    // await _appService.PegarRelatorioPorIdUsuario(id);


    // [HttpPost("Login")]
    // public IActionResult Login(string email, string senha)
    // {
    //     try
    //     {
    //         if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(senha))
    //         {
    //             return BadRequest("Erro");
    //         }

    //         if (email == "nicolas" && senha == "123")
    //         {
    //             var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["jwt:Key"]!));
    //             var sign = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
    //             var jwtToken = new JwtSecurityToken(

    //                 issuer: _config["jwt:Issuer"],
    //                 audience: _config["jwt:Audience"],
    //                 claims: new List<Claim>(),
    //                 expires: DateTime.Now.AddHours(1),
    //                 signingCredentials: sign
    //                 );

    //             return Ok(new JwtSecurityTokenHandler().WriteToken(jwtToken));
    //         }
    //     }
    //     catch (System.Exception)
    //     {
    //         return BadRequest("Erro");
    //     }
    //     return Unauthorized();
    // }
}
