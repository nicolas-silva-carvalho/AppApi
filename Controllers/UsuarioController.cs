using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ApiAPP.Services;
using ApiAPP.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using MongoDB.Driver.Core.Configuration;
using MongoDB.Bson;

namespace ApiAPP.Controllers;

[ApiController]
[Route("[controller]")]

public class UsuarioController : ControllerBase
{
    private readonly IConfiguration _config;
    private readonly AppService _appService;
    public UsuarioController(IConfiguration config, AppService appService)
    {
        _config = config;
        _appService = appService;
    }

    [HttpPost("Login")]
    public IActionResult Login(string email, string senha)
    {
        try
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(senha))
            {
                return BadRequest("Erro");
            }

            if (email == "nicolas" && senha == "123")
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

                return Ok(new JwtSecurityTokenHandler().WriteToken(jwtToken));
            }
        }
        catch (System.Exception)
        {
            return BadRequest("Erro");
        }
        return Unauthorized();
    }

    [HttpGet("MongoPrefeitura")]
    public string DBPrefeitura()
    {
        var settings = new MongoClientSettings()
        {
            Scheme = ConnectionStringScheme.MongoDB,
            Server = new MongoServerAddress("172.30.101.233", 27017),
            Credential = MongoCredential.CreateCredential("admin", "mongoadmin", "q1w2e3r4$"),
            AllowInsecureTls = true
        };

        var client = new MongoClient(settings);


        return client.ListDatabaseNames().ToList().ToJson();
    }

    [HttpGet]
    public async Task<List<Relatorio>> Get() =>
        await _appService.PegarUsuario();

    [HttpGet("id")]
    public async Task<ActionResult<Relatorio>> Get(string id)
    {
        var relatorio = await _appService.PegarUsuarioId(id);

        if (relatorio is null)
        {
            return NotFound();
        }

        return relatorio;
    }

    [HttpPost]
    public async Task<IActionResult> Post(Relatorio relatorio)
    {
        await _appService.Criar(relatorio);

        return CreatedAtAction(nameof(Get), new { id = relatorio.Id }, relatorio);
    }

    [HttpPut("id")]
    public async Task<IActionResult> Update(string id, Relatorio atualizarRelatorio)
    {
        var relatorio = await _appService.PegarUsuarioId(id);

        if (relatorio is null)
        {
            return NotFound();
        }

        atualizarRelatorio.Id = relatorio.Id;

        await _appService.Atualizar(id, atualizarRelatorio);

        return NoContent();
    }

    [HttpDelete("id")]
    public async Task<IActionResult> Delete(string id)
    {
        var relatorio = await _appService.PegarUsuarioId(id);

        if (relatorio is null)
        {
            return NotFound();
        }

        await _appService.Remover(id);

        return NoContent();
    }
}
