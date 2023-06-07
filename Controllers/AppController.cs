using ApiAPP.Services;
using ApiAPP.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using MongoDB.Driver.Core.Configuration;
using MongoDB.Bson;
using Microsoft.AspNetCore.Authorization;

namespace ApiAPP.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]

public class AppController : ControllerBase
{
    private readonly AppService _appService;
    private readonly IHttpContextAccessor _ihttpContextAccessor;
    public AppController(AppService appService, IHttpContextAccessor ihttpContextAccessor)
    {
        _ihttpContextAccessor = ihttpContextAccessor;
        _appService = appService;
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
    public async Task<IActionResult> Post(Relatorio relatorio)
    {
        var user = _ihttpContextAccessor.HttpContext.User.Claims.ToList();
        await _appService.CriarRelatorio(relatorio);

        return CreatedAtAction(nameof(Get), new { id = relatorio.Id }, relatorio);
    }

    [HttpPut("id")]
    public async Task<IActionResult> Update(string id, Relatorio atualizarRelatorio)
    {
        var relatorio = await _appService.PegarRelatorioId(id);

        if (relatorio is null)
        {
            return NotFound();
        }

        atualizarRelatorio.Id = relatorio.Id;

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
