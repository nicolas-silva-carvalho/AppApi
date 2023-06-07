using ApiAPP.Services;
using ApiAPP.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using MongoDB.Driver.Core.Configuration;
using MongoDB.Bson;
using Microsoft.AspNetCore.Authorization;
using AppApi.Services;
using AutoMapper;
using ApiAPP.DTOs;

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
    public async Task<IActionResult> Login(UsuarioRequest request)
    {
        try
        {
            var usuario = await _usuarioService.PegarUsuarioPorNomeESenha(request.NomeUsuario, request.SenhaLogin);
            if (usuario == null) return Unauthorized("Usuário inválido.");

            var response = _mapp.Map<UsuarioResponse>(usuario);

            response.Token = _token.CriarToken(usuario).Result;

            return Ok(response);
        }
        catch (Exception ex)
        {
            return this.StatusCode(StatusCodes.Status500InternalServerError,
             $"Erro ao tentar recuperar usuários. Erro: {ex.Message}");
        }
    }

    [HttpGet]
    public async Task<List<Usuario>> Get() =>
        await _usuarioService.PegarUsuariosTotal();

    [HttpPost("Registrar")]
    public async Task<IActionResult> Registrar(Usuario usuario)
    {
        await _usuarioService.CriarUsuario(usuario);

        return CreatedAtAction(nameof(Get), new { id = usuario.Id }, usuario);
    }
}
