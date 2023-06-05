using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiAPP.Models;

namespace AppApi.Services
{
    public interface IToken
    {
        Task<string> CriarToken(UsuarioLogin usuario);
    }
}