using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiAPP.test
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nome { get; set; } = default!;
        public string Telefone { get; set; } = default!;
        public string FotoUrl { get; set; } = default!; //TODO: verificar depois
        public List<Relatorio2> Relatorios { get; set; } = default!;
    }
}