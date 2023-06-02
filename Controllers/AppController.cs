using ApiAPP.Data;
using ApiAPP.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiAPP.Controllers;

[ApiController]
[Route("[controller]")]
public class AppController : ControllerBase
{
    [HttpGet]
    public string get()
    {
        return "Asdasdas";
    }
    // private readonly Context _context;
    // public AppController(Context context)
    // {
    //     _context = context;
    // }

    // [HttpGet]
    // public async Task<ActionResult<Relatorio[]>> Get()
    // {
    //     IEnumerable<Relatorio> relatorio = _context.Relatorios.ToList();
    //     return Ok(relatorio);
    // }

    // [HttpGet("{id}")]
    // public async Task<ActionResult<Relatorio>> GetId(string id)
    // {
    //     var relatorio = _context.Relatorios.FirstOrDefault(x => x.Id == id);
    //     return Ok(relatorio);
    // }

    // [HttpPost]
    // public async Task<ActionResult<Relatorio>> Post(Relatorio relatorio)
    // {
    //     _context.Add(relatorio);
    //     _context.SaveChanges();
    //     return Ok(relatorio);
    // }
}
