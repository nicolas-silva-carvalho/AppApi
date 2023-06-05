using ApiAPP.Services;
using ApiAPP.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using MongoDB.Driver.Core.Configuration;
using MongoDB.Bson;
using Microsoft.AspNetCore.Authorization;

namespace ApiAPP.Controllers;

[ApiController]
[Route("[controller]")]

public class AppController : ControllerBase
{
    private readonly AppService _appService;
    public AppController(AppService appService)
    {
        _appService = appService;
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
        await _appService.PegarRelatoriosTotal();
        

    // [HttpGet("id")]
    // public async Task<List<Relatorio>> Get(string id)
    // {
    //     var relatorio = await _appService.PegarRelatorioPorIdUsuario(id);
    //     return relatorio;
    // }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Post(Relatorio relatorio)
    {
        await _appService.CriarRelatorio(relatorio);

        return CreatedAtAction(nameof(Get), new { id = relatorio.RelatorioId }, relatorio);
    }

    [HttpPut("id")]
    public async Task<IActionResult> Update(string id, Relatorio atualizarRelatorio)
    {
        var relatorio = await _appService.PegarRelatorioId(id);

        if (relatorio is null)
        {
            return NotFound();
        }

        atualizarRelatorio.RelatorioId = relatorio.RelatorioId;

        await _appService.AtualizarRelatorio(id, atualizarRelatorio);

        return NoContent();
    }

    [HttpDelete("id")]
    public async Task<IActionResult> Delete(string id)
    {
        var relatorio = await _appService.PegarRelatorioId(id);

        if (relatorio is null)
        {
            return NotFound();
        }

        await _appService.RemoverRelatorioPorId(id);

        return NoContent();
    }
}
