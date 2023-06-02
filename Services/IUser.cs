using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiAPP.Models;

namespace ApiAPP.Services
{
    public interface IUser
    {
        Task<IEnumerable<Usuario>> PegarUsuario();
        Task<Usuario> PegarUsuarioPorId(int id);
        Task<Usuario> PegarUsuarioPorNome(string nomeUsuario);
    }
}