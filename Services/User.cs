using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiAPP.Data;
using ApiAPP.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiAPP.Services
{
    public class User
    {
        // private readonly Context _context;

        // public User(Context context)
        // {  
        //     _context = context;
        // }
        // public async Task<IEnumerable<Usuario>> PegarUsuario()
        // {
        //     return await _context.Usuario.ToListAsync();
        // }

        // public async Task<Usuario> PegarUsuarioPorId(int id)
        // {
        //     return await _context.Usuario.FindAsync(id);
        // }

        // public async Task<Usuario> PegarUsuarioPorNome(string nomeUsuario)
        // {
        //     return await _context.Usuario.SingleOrDefaultAsync(nome => nome.Nome == nomeUsuario.ToLower());
        // }
    }
}