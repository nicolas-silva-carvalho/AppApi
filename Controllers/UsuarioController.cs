using ApiAPP.Services;
using ApiAPP.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using MongoDB.Driver.Core.Configuration;
using MongoDB.Bson;
using Microsoft.AspNetCore.Authorization;
using AppApi.Services;
using AutoMapper;

[ApiController]
[Route("[controller]")]
public class UsuarioController : ControllerBase
{
    private readonly IToken _token;
    private readonly UsuarioService _usuarioService;
    private readonly IMapper _mapp;
    public UsuarioController(IToken token, UsuarioService usuarioService, IMapper mapp)
    {
        _mapp = mapp;
        _usuarioService = usuarioService;
        _token = token;
    }

    [HttpPost("Login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login(UsuarioLogin usuario)
    {
        try
        {
            var login = _mapp.Map<Usuario>(usuario);

            var usuarioNome = await _usuarioService.PegarUsuarioPorNome(usuario.NomeUsuario);
            if(usuarioNome == null) return Unauthorized("Usuário inválido.");

            var usuarioSenha = await _usuarioService.PegarUsuarioPorSenha(usuario.Senha);
            if(usuarioSenha == null) return Unauthorized();

            return Ok(new 
            {
                nomeUsuario  = login.Nome,
                email = login.Email,
                token = _token.CriarToken(usuario).Result
            });
        }
        catch (Exception ex)
        {
           return this.StatusCode(StatusCodes.Status500InternalServerError,
            $"Erro ao tentar recuperar usuários. Erro: {ex.Message}");
        }
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<List<Usuario>> Get() =>
        await _usuarioService.PegarUsuariosTotal();

    [HttpPost("Registrar")]
    [AllowAnonymous]
    public async Task<IActionResult> Registrar(Usuario usuario)
    {
        await _usuarioService.CriarUsuario(usuario);

        return CreatedAtAction(nameof(Get), new { id = usuario.UsuarioId }, usuario);
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
